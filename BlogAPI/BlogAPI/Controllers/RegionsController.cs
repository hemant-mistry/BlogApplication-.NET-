using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Models.Domain;
using BlogAPI.Data;
using BlogAPI.Models.DTO;

namespace BlogAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext) 
        {
			this.dbContext = dbContext;
            
        }
        [HttpGet]
		public IActionResult GetAll()
		{
			//Get Data from Database - Domain models
			var regions = dbContext.Regions.ToList();

			//Map Domain Models to DTOs

			var regionsDto = new List<RegionDto>();

			foreach (var region in regions)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl,
				});
				
			}

			//Return DTOs
			return Ok(regionsDto);

		}

		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			var region = dbContext.Regions.Find(id);

			if (region == null)
			{
				return NotFound();
			}

			//Map Region Domain Model to Region DTO
			var regionsDto = new RegionDto
			{
				Id = region.Id,
				Code = region.Code,
				Name = region.Name
			};

			return Ok(regionsDto);
			
		}

		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			//Map or Convert DTO to Domain Model

			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name
			};

			dbContext.Regions.Add(regionDomainModel);
			dbContext.SaveChanges();


			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name
			};
			return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);





		}
	}
}
