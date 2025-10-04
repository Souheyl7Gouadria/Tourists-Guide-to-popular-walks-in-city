namespace Application.API.Models.DTOs.RegionDTOs
{
    public class AddRegionRequestDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
