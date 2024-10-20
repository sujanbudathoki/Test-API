using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_API.Domain.DTO;
using test_API.Domain.Entities;
using test_API.Domain.Repository;
using test_API.Service.IServices;
using test_API.Service.Mappings;

namespace test_API.Service.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task CreateOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            var existingCandidate = await _candidateRepository.GetCandidateByEmailAsync(candidateDto.Email);

            if (existingCandidate == null)
            {
                var newCandidate = Mapping.MapDtoToCandidate(candidateDto);          

                await _candidateRepository.AddCandidateAsync(newCandidate);
            }
            else
            {
                UpdateCandidateFromDto(existingCandidate, candidateDto);

                await _candidateRepository.UpdateCandidateAsync(existingCandidate);
            }
        }

        public async Task<CandidateDto?> GetCandidateByEmailAsync(string email)
        {
            var candidate = await _candidateRepository.GetCandidateByEmailAsync(email);
            return candidate == null ? null : Mapping.MapCandidateToDto(candidate);
        }

    
        private void UpdateCandidateFromDto(Candidate existingCandidate, CandidateDto candidateDto)
        {
            existingCandidate.FirstName = candidateDto.FirstName;
            existingCandidate.LastName = candidateDto.LastName;
            existingCandidate.PhoneNumber = candidateDto.PhoneNumber;
            existingCandidate.PrefferedCallTime = candidateDto.TimeToCall;
            existingCandidate.LinkedinProfileURL = candidateDto.LinkedInUrl;
            existingCandidate.GitHubProfileURL = candidateDto.GitHubUrl;
            existingCandidate.Comments = candidateDto.Comments;
        }

        public async Task<IEnumerable<Candidate>?> GetAllCandidatesAsync() // Implement the method
        {
            return await _candidateRepository.getAllCandidates();
        }

    }
}
