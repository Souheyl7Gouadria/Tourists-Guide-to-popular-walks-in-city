using Application.API.Models.DTOs.RegionDTOs;
using Application.API.Models.DTOs.DifficultyDTOs;
namespace Application.API.Models.DTOs.WalkDTOs
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }

        }
}
