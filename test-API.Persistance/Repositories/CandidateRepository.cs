using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_API.Domain.Entities;
using test_API.Domain.Repository;
using test_API.Data;
using Microsoft.EntityFrameworkCore;

namespace test_API.Data.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDBContext _context;

        public CandidateRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }

}
