namespace Delivery.Application.Services;

public interface IBlobStorageService
{
    Task<Uri> UploadFileAsync(string localFilePath, string blobName);
    Task<Uri> UploadStreamAsync(Stream stream, string blobName, string contentType = null);
}