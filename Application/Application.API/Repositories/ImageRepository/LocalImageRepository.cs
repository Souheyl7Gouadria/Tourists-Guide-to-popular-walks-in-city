using Application.API.Data;
using Application.API.Models.Domain;

namespace Application.API.Repositories.ImageRepository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _appDbContext = appDbContext;

        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
        $"{image.FileName}{image.FileExtension}");
            // create a write stream to the file path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            // copy the file to the server disk
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add image to Images table
            await _appDbContext.Images.AddAsync(image);
            await _appDbContext.SaveChangesAsync();

            return image;
        }
    }
}
