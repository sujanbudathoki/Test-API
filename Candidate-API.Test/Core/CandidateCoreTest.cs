using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using test_API.Data;
using test_API.Data.Repository;
using test_API.Domain.Entities;
using test_API.Domain.Repository;

namespace Candidate_API.Test.Core
{
    public class CandidateCoreTest
    {
        private AppDBContext _context;
        private ICandidateRepository _repository;

        [SetUp]
        public void Setup()
        {
            // Set up the in-memory database
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDBContext(options);
            _repository = new CandidateRepository(_context);
        }

        [Test]
        public async Task GetCandidateByEmailAsync_ReturnsCandidate_WhenEmailExists()
        {
            // Arrange
            var candidate = new Candidate
            {
                Email = "test3@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                PrefferedCallTime = "2 PM - 3 PM",
                LinkedinProfileURL = "http://linkedin.com/in/test",
                GitHubProfileURL = "http://github.com/test",
                Comments = "Test comment"
            };

            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCandidateByEmailAsync("test3@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(candidate.FirstName, result.FirstName);
            Assert.AreEqual(candidate.LastName, result.LastName);
        }

        [Test]
        public async Task GetCandidateByEmailAsync_ReturnsNull_WhenEmailDoesNotExist()
        {
            // Act
            var result = await _repository.GetCandidateByEmailAsync("nonexistent@example.com");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddCandidateAsync_AddsCandidate_WhenCalled()
        {
            // Arrange
            var candidate = new Candidate
            {
                Email = "new@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                PhoneNumber = "0987654321",
                PrefferedCallTime = "3 PM - 4 PM",
                LinkedinProfileURL = "http://linkedin.com/in/new",
                GitHubProfileURL = "http://github.com/new",
                Comments = "New candidate"
            };

            // Act
            await _repository.AddCandidateAsync(candidate);
            var result = await _repository.GetCandidateByEmailAsync(candidate.Email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(candidate.FirstName, result.FirstName);
            Assert.AreEqual(candidate.LastName, result.LastName);
        }
    }
}
