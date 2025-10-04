using Application.API.Models.Domain;
using Application.API.Models.DTOs;
namespace Application.API.Repositories.RegionRepository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumer = 1, int pageSize = 20);
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
