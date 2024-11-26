using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels;
using Service.Interfaces;
using Utility.Constants;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/media")]
    [ApiController]
    [Authorize]
    public class MediaController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService = serviceProvider.GetRequiredService<ICloudinaryService>();

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(file);
            return Ok(BaseResponse.OkResponseDto(imageUrl));
        }

        [HttpDelete("image")]
        public async Task<IActionResult> DeleteImage([FromQuery] string imageUrl)
        {
            await _cloudinaryService.DeleteImageAsync(imageUrl);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageConstantsCommon.SUCCESS));
        }
    }
}
