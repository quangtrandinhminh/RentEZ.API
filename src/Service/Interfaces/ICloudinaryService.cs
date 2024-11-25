using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task DeleteImageAsync(string imageUrl);
}