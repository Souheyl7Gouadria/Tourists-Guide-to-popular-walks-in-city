using Application.API.Models.Domain;
using Application.API.Models.DTOs.ImageDTOs;
using Application.API.Repositories.ImageRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        // POST: /api/Images/Upload
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Convert DTO to Domain mmodel
            var imageDomainModel = new Image
            {
                File = request.File,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            // Use repository to upload image
            await _imageRepository.Upload(imageDomainModel);
            return Ok(imageDomainModel);
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
