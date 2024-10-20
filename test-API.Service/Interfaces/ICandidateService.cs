using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_API.Domain.DTO;
using test_API.Domain.Entities;

namespace test_API.Service.IServices
{
    public interface ICandidateService
    {
      
            Task<CandidateDto?> GetCandidateByEmailAsync(string email);
            Task CreateOrUpdateCandidateAsync(CandidateDto candidate);
            Task<IEnumerable<Candidate?>> GetAllCandidatesAsync();
     
    }
}
