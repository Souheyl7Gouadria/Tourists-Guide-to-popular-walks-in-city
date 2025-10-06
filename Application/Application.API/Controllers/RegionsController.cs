using Application.API.CustomActionFilters;
using Application.API.Data;
using Application.API.Models.Domain;
using Application.API.Models.DTOs.RegionDTOs;
using Application.API.Repositories.RegionRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(AppDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            // domain models , from DB
            var regions = await _regionRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            // map domain models to DTOs
            var regionsDTO = _mapper.Map<List<RegionDTO>>(regions);
            // return DTOs
            return Ok(regionsDTO);
        }

        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            // get region domain model from DB by id
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null) return NotFound();
            // map region domain model to DTO
            var regionDTO = _mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO regionRequestDTO)
        {
            // map DTO to domain model
            var regionDomainModel = _mapper.Map<Region>(regionRequestDTO);
            // use domain model to create region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            // map domain model back to DTO
            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(CreateRegion), new { id = regionDomainModel.Id }, regionDTO);
        }

        [HttpPut("{id}")]
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDTO);
            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null) return NotFound();
            // Convert domain model to DTO
            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);
            if (regionDomainModel == null) return NotFound();
            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);
        }

    }
}
