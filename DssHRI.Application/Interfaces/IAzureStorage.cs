using DssHRI.Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace DssHRI.application.Interfaces;

public interface IAzureStorage
{
    Task<BlobResponseDto> UploadAsync(IFormFile file);
    Task<BlobResponseDto> UploadWithSASUrlAsync(IFormFile file, string sasUrl);
    Task<BlobResponseDto> UploadAsync(Stream content, string contentType, string fileName);
    Task<BlobDto> DownloadAsync(string blobFilename);
    Task<BlobResponseDto> DeleteAsync(string blobFilename);
    Task<List<BlobDto>> ListAsync();
    Task<Uri> GetServiceSasUriForContainer(string storedPolicyName = null);
    //Task<Uri> GetServiceSasUriForContainer(BlobContainerClient containerClient, string storedPolicyName = null);
    //Task<Uri> GetServiceSasUriForBlob(BlobClient blobClient, string storedPolicyName = null);

}
