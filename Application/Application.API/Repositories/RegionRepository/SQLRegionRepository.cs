using Application.API.Data;
using Application.API.Models.Domain;
using Application.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Application.API.Repositories.RegionRepository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLRegionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 20)
        {
            var regions = _dbContext.Regions.AsQueryable();
            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                regions = regions.Where(x => x.Name.Contains(filterQuery));
            }
            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending.HasValue && isAscending.Value ? regions.OrderBy(x => x.Name) : regions.OrderByDescending(x => x.Name);
                }
            }
            // Pagination
            var skipResults = (pageNumber-1)* pageSize;
            return await regions.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.AddAsync(region);
            _dbContext.SaveChanges();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) return null;
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionToDelete = await _dbContext.Regions.FindAsync(id);
            if (regionToDelete == null) return null;
            _dbContext.Remove(regionToDelete);
            await _dbContext.SaveChangesAsync();
            return regionToDelete;
        }
    }
}
