using Application.API.Models.Domain;

namespace Application.API.Repositories.ImageRepository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
