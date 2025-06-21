using Delivery.Comunication.RequestModel.ElasticSearch;
using Nest;

namespace Delivery.Extensions;


public class Elasticsearch
{
    private readonly IElasticClient _client;
    private const string IndexName = "products_ecommerce"; // Defina um nome para seu índice

    public Elasticsearch(string elasticsearchUrl = "http://localhost:9200")
    {
        var settings = new ConnectionSettings(new Uri(elasticsearchUrl))
            .DefaultIndex(IndexName) // Define um índice padrão para as operações
            .EnableDebugMode() // Útil para ver as queries enviadas ao ES (remover em produção)
            .PrettyJson()      // Formata o JSON das requisições/respostas (remover em produção)
            .OnRequestCompleted(apiCallDetails => // Loggar requisições/respostas
            {
                // Loggar detalhes aqui se necessário, por exemplo, para um logger como Serilog/NLog
                Console.WriteLine($@"ES Request: {apiCallDetails.DebugInformation}");
            });

        _client = new ElasticClient(settings);
    }

    public async Task CreateIndexIfNotExistsAsync()
    {
        var indexExistsResponse = await _client.Indices.ExistsAsync(IndexName);
        if (indexExistsResponse.Exists)
        {
            Console.WriteLine($@"Índice '{IndexName}' já existe.");
            return;
        }

        Console.WriteLine($@"Criando índice '{IndexName}'...");
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
            .Map<ProductDocument>(m => m // Mapeia a classe ProductDocument para o tipo padrão do índice
                .AutoMap() // Tenta mapear automaticamente com base nos atributos da class
            )
        );

        if (createIndexResponse.IsValid)
        {
            Console.WriteLine($"Índice '{IndexName}' criado com sucesso.");
        }
        else
        {
            Console.WriteLine($"Falha ao criar índice '{IndexName}': {createIndexResponse.DebugInformation}");
            // Lançar exceção ou tratar o erro apropriadamente
            throw new System.Exception($"Falha ao criar índice: {createIndexResponse.ServerError?.Error?.Reason}");
        }
    }
}