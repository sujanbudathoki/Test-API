using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_API.Domain.DTO;
using test_API.Domain.Entities;

namespace test_API.Service.Mappings
{
    public static class Mapping
    {
        public static CandidateDto MapCandidateToDto(Candidate candidate)
        {
            return new CandidateDto
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                TimeToCall = candidate.PrefferedCallTime,
                LinkedInUrl = candidate.LinkedinProfileURL,
                GitHubUrl = candidate.GitHubProfileURL,
                Comments = candidate.Comments
            };
        }

        public static Candidate MapDtoToCandidate(CandidateDto candidateDto)
        {
            return new Candidate
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                Email = candidateDto.Email,
                PhoneNumber = candidateDto.PhoneNumber,
                PrefferedCallTime = candidateDto.TimeToCall,
                LinkedinProfileURL = candidateDto.LinkedInUrl,
                GitHubProfileURL = candidateDto.GitHubUrl,
                Comments = candidateDto.Comments
            };
        }


    }
}
