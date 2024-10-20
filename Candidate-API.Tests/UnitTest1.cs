using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using test_API.API.Controllers;
using test_API.Domain.Entities;
using test_API.Domain.Services;
using test_API.Persistance; // Update this namespace according to your project structure

namespace test_API.Tests
{
    public class CandidatesControllerTests
    {
        private CandidatesController _controller;
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _controller = new CandidatesController(new CandidateService(new CandidateRepository(_context)));
        }

        [Test]
        public async Task CreateOrUpdate_ShouldInsertNewCandidate()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                TimeToCall = "2 PM - 3 PM",
                LinkedInUrl = "http://linkedin.com/in/test",
                GitHubUrl = "http://github.com/test",
                Comments = "Test comment"
            };

            // Act
            var result = await _controller.CreateOrUpdate(candidateDto);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
            var savedCandidate = await _context.Candidates.FindAsync(candidateDto.Email);
            Assert.That(savedCandidate, Is.Not.Null);
            Assert.That(savedCandidate.FirstName, Is.EqualTo(candidateDto.FirstName));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllCandidates()
        {
            // Arrange
            var candidates = new List<Candidate>
            {
                new Candidate { Email = "test1@example.com", FirstName = "John", LastName = "Doe" },
                new Candidate { Email = "test2@example.com", FirstName = "Jane", LastName = "Doe" }
            };
            await _context.Candidates.AddRangeAsync(candidates);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(candidates));
        }

        [Test]
        public async Task GetByEmail_ShouldReturnNotFound_WhenCandidateDoesNotExist()
        {
            // Act
            var result = await _controller.GetByEmail("nonexistent@example.com");

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetByEmail_ShouldReturnOk_WithCandidate_WhenFound()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "John", LastName = "Doe" };
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetByEmail(candidate.Email);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(candidate));
        }
    }
}
