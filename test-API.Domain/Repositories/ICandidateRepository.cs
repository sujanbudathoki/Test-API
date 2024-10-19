using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_API.Domain.Entities;

namespace test_API.Domain.Repository
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetCandidateByEmailAsync(string email);
        Task AddCandidateAsync(Candidate candidate);
        Task UpdateCandidateAsync(Candidate candidate);
    }
}
