using Application.API.Models.Domain;
using Application.API.Models.DTOs.DifficultyDTOs;
using Application.API.Models.DTOs.RegionDTOs;
using Application.API.Models.DTOs.WalkDTOs;
using Application.API.Repositories.WalkRepository;
using AutoMapper;

namespace Application.API.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO,Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();
            CreateMap<DifficultyDTO, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();
        }
    }
}
