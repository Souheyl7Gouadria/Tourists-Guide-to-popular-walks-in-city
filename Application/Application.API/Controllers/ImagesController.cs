using Application.API.Models.DTOs.ImageDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Use repository to upload image
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new[] {".jpg", ".jpeg", ".png"};
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (request.File.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "File size exceeds the 5MB limit");
            }
        }
    }
}
