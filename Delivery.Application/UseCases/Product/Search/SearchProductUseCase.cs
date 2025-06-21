using Delivery.Comunication.RequestModel.ElasticSearch;
using Microsoft.Extensions.Logging;
using Nest;

namespace Delivery.Application.UseCases.Product.Search;

public class SearchProductUseCase(ILogger<SearchProductUseCase> logger) : ISearchProductUseCase
{
    private readonly IElasticClient _client;
    private const string IndexName = "products"; // Defina um nome para seu índice

    public async Task<ProductDocument> ExecuteAsync(string query)
    {
        await CreateIndexIfNotExistsAsync();
        var result = await SugerirProdutos(query);

        return new ProductDocument();
    }

    public SearchProductUseCase(ILogger<SearchProductUseCase> logger, string elasticsearchUrl = "http://localhost:9200") : this(logger)
    {

        var settings = new ConnectionSettings(new Uri(elasticsearchUrl))
            .DefaultIndex(IndexName) // Define um índice padrão para as operações
            .EnableDebugMode() // Útil para ver as queries enviadas ao ES (remover em produção)
            .PrettyJson() // Formata o JSON das requisições/respostas (remover em produção)
            .OnRequestCompleted(apiCallDetails => // Loggar requisições/respostas
            {
                
            });

        _client = new ElasticClient(settings);
    }

    private async Task CreateIndexIfNotExistsAsync()
    {
        var indexExistsResponse = await _client.Indices.ExistsAsync(IndexName);
      
        if (indexExistsResponse.Exists)
        {
            logger.LogInformation($"Índice '{IndexName}' já existe.");
            return;
        }

        logger.LogInformation($"Criando índice '{IndexName}'...");
        
        // var createIndexResponse = await _client.Indices.CreateAsync("products", c => c
        //     .Map<Product>(m => m
        //         .Properties(p => p
        //                 .Keyword(k => k.Name(n => n.Id))
        //                 .Text(t => t.Name(n => n.Name).Analyzer("brazilian")) // Exemplo de analisador
        //                 .Text(t => t.Name(n => n.Description).Analyzer("brazilian"))
        //                 .DenseVector(dv => dv.Name(n => n.NameEmbedding).Dims(768)) // Dimensões do vetor
        //                 .DenseVector(dv => dv.Name(n => n.DescriptionEmbedding).Dims(768))
        //             // ... outros mapeamentos
        //         )
        //     )
        // );
        
        
        
        var createIndexResponse = await _client.Indices.CreateAsync(IndexName, c => c
                
            .Settings(s => s
                .Analysis(a => a
                    .Analyzers(an => an
                        .Custom("portuguese_analyzer", ca => ca
                            .Tokenizer("standard")
                            .Filters("lowercase", "brazilian_stop_filter", "brazilian_stem_filter")
                        )
                    )
                    .TokenFilters(tf => tf // Definindo os filtros se não usar plugin ICU
                        .Stop("brazilian_stop_filter", st => st
                                .StopWords("_brazilian_") // Palavras de parada para português
                        )
                        .Stemmer("brazilian_stem_filter", st => st
                                .Language("brazilian") // Stemmer para português
                        )
                    )
                )
            )
            .Map<Domain.Entities.Product>(m => m
                .Properties(p => p
                    .Keyword(k => k.Name(n => n.Id))
                    .Text(t => t.Name(n => n.Name).Analyzer("brazilian")) // Exemplo de analisador
                    .Text(t => t.Name(n => n.Description).Analyzer("brazilian"))
                )
            )
        );
        
        if (createIndexResponse.IsValid)
        {
          logger.LogInformation($"Índice '{IndexName}' criado com sucesso.");
        }
        else
        {
            logger.LogInformation($"Falha ao criar índice '{IndexName}': {createIndexResponse.DebugInformation}");
          
            throw new System.Exception($"Falha ao criar índice: {createIndexResponse.ServerError?.Error?.Reason}");
        }
    }
    
    public async Task<List<Domain.Entities.Product>> SearchProducts(string queryText)
    {
        // 1. Gerar o embedding da consulta
        // var queryEmbedding = await _embeddingService.GenerateEmbeddingAsync(queryText);

        var searchResponse = await _client.SearchAsync<Domain.Entities.Product>(s => s
                .Index("products")
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            // Busca por texto completo para relevância de palavras-chave
                            bs => bs.Match(m => m
                                    .Field(f => f.Name)
                                    .Query(queryText)
                                    .Boost(2.0) // Mais peso para o nome
                            ),
                            bs => bs.Match(m => m
                                .Field(f => f.Description)
                                .Query(queryText)
                            ),
                            // Busca por similaridade de vetores (semântica)
                            bs => bs.ScriptScore(ss => ss
                                    .Query(q => q.MatchAll()) // Busca em todos os documentos para o script_score
                                    .Script(script => script
                                        .Source("cosineSimilarity(params.queryVector, 'NameEmbedding') + cosineSimilarity(params.queryVector, 'DescriptionEmbedding')")
                                        // .Params(p => p.Add("queryVector", queryEmbedding))
                                    )
                                    // .MinScore(0.5) // Limiar de similaridade para considerar relevante
                                    .Boost(3.0) // Mais peso para a busca semântica
                            )
                        )
                    )
                )
                .Size(20) // Quantidade de resultados
        );

        return searchResponse.Documents.ToList();
    }
    
    private async Task<ISearchResponse<ProductDocument>> SugerirProdutos(string prefixo)
    {
        var response = await _client.SearchAsync<ProductDocument>(s => s
            .Suggest(su => su
                    .Completion("telefone", cs => cs // Precisa de um campo mapeado como 'completion'
                        .Field(f => f.Name) // Ou um campo específico para sugestões
                        .Prefix(prefixo)
                        .Fuzzy(f => f.Fuzziness(Fuzziness.Auto))
                        .Size(5)
                    )
                // Você também pode usar .Query(q => q.MatchPhrasePrefix(...)) para um autocomplete mais simples
                // se não quiser configurar um 'completion suggester'
            )
        );
        
        return response;
    }
}