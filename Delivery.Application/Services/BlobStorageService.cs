using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Delivery.Application.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

   

    public async Task<Uri> UploadFileAsync(string localFilePath, string blobName)
    {
        if (string.IsNullOrWhiteSpace(blobName))
        {
            // Gerar um nome único se não for fornecido, ou usar o nome do arquivo local
            blobName = Path.GetFileName(localFilePath);
        }

        // 5. Obter o BlobClient para o arquivo específico
        BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);

        Console.WriteLine($"Fazendo upload para o Azure Blob Storage como blob:\n\t{blobClient.Uri}\n");

        try
        {
            // 6. Fazer o upload do arquivo
            // Use File.OpenRead para obter um FileStream do arquivo local
            await using (FileStream uploadFileStream = File.OpenRead(localFilePath))
            {
                // O parâmetro 'overwrite: true' substitui o blob se ele já existir
                await blobClient.UploadAsync(uploadFileStream, overwrite: true);
            }

            Console.WriteLine($"Upload concluído para: {blobClient.Uri}");
            return blobClient.Uri;
        }
        catch (Azure.RequestFailedException ex)
        {
            Console.WriteLine($"Erro durante o upload: {ex.Message}");
            // Verifique o ex.ErrorCode para mais detalhes, ex: "AuthorizationPermissionMismatch"
            throw;
        }
    }

    public async Task<Uri> UploadStreamAsync(Stream stream, string blobName, string contentType = null)
    {
         if (stream == null || stream.Length == 0)
        {
            throw new ArgumentException("O stream não pode ser nulo ou vazio.", nameof(stream));
        }
        if (string.IsNullOrWhiteSpace(blobName))
        {
            throw new ArgumentException("O nome do blob não pode ser nulo ou vazio.", nameof(blobName));
        }

        // Resetar a posição do stream para o início, caso já tenha sido lido
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);
        Console.WriteLine($"Fazendo upload do stream para o Azure Blob Storage como blob:\n\t{blobClient.Uri}\n");

        try
        {
            var uploadOptions = new BlobUploadOptions();
            if (!string.IsNullOrEmpty(contentType))
            {
                uploadOptions.HttpHeaders = new BlobHttpHeaders { ContentType = contentType };
            }

            await blobClient.UploadAsync(stream, uploadOptions);

            Console.WriteLine($"Upload do stream concluído para: {blobClient.Uri}");
            return blobClient.Uri;
        }
        catch (Azure.RequestFailedException ex)
        {
            Console.WriteLine($"Erro durante o upload do stream: {ex.Message}");
            throw;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Erro durante o upload do stream: {ex.Message}");
            throw;
        }
    }
}