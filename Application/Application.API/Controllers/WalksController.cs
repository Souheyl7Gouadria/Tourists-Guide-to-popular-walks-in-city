using Application.API.CustomActionFilters;
using Application.API.Models.Domain;
using Application.API.Models.DTOs.WalkDTOs;
using Application.API.Repositories.WalkRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        // Get: /api/walks?filterOn=Name&filterQuery=track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var walksDomainModels = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var walksDTO = _mapper.Map<List<WalkDTO>>(walksDomainModels);
            return Ok(walksDTO);
        }

        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO walkRequestDTO)
        {
            // Map DTO to domain model
            var walkDomainModel = _mapper.Map<Walk>(walkRequestDTO);
            await _walkRepository.CreateAsync(walkDomainModel);

            // Map domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById([FromQuery] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null) return NotFound();
            // Map domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpPut("{id}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Update(Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            // Map updateWalkRequestDTO to domain model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDTO);
            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null) return NotFound();
            // map domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomainModel = await _walkRepository.DeleteAsync(id);
            // Map domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }
    }
}
