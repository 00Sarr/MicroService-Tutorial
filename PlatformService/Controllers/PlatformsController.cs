using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetAllPlatforms()
        {
            var platforms = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
        }

        [HttpGet("{id}", Name ="GetPlatformByID")]
        public ActionResult<PlatformReadDTO> GetPlatformByID(int id)
        {
            var platforms = _repo.GetPlatformById(id);
            if (platforms != null)
                return Ok(_mapper.Map<PlatformReadDTO>(platforms));
            else
                return NotFound();
        }
        [HttpPost]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatformCreateDTO platformCreate)
        {
            var platformModel = _mapper.Map<Platform>(platformCreate);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();
            var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformByID), new { platformReadDTO.Id },platformReadDTO);
        }
    }
}
