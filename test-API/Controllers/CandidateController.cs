using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using test_API.Domain.DTO;
using test_API.Domain.Entities;
using test_API.Service.IServices;
using test_API.Service.Mappings;

namespace test_API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        // Create or update candidate
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CandidateDto candidateDto)
        {
            var candidate = Mapping.MapDtoToCandidate(candidateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateService.CreateOrUpdateCandidateAsync(candidateDto);
            return Ok();
        }

        // Get all candidates
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates); 
        }

        // Get candidate by email
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var candidate = await _candidateService.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound(); 
            }
            return Ok(candidate);
        }
    }
}
