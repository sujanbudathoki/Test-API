using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using test_API.Data;
using test_API.Data.Repository;
using test_API.Domain.DTO;
using test_API.Domain.Entities;
using test_API.Persistance;
using test_API.Service.Services;

namespace Candidate_API.Test.Service
{
    public class CandidateServiceTests
    {
        private CandidateService _service;
        private CandidateRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "Candidate")
                .Options;

            var context = new AppDBContext(options);
            _repository = new CandidateRepository(context);
            _service = new CandidateService(_repository);
        }

        [Test]
        public async Task CreateOrUpdateCandidateAsync_ShouldAddNewCandidate_WhenNotExists()
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
            await _service.CreateOrUpdateCandidateAsync(candidateDto);

            // Assert
            var savedCandidate = await _repository.GetCandidateByEmailAsync(candidateDto.Email);
            Assert.That(savedCandidate, Is.Not.Null);
            Assert.That(savedCandidate.FirstName, Is.EqualTo(candidateDto.FirstName));
            Assert.That(savedCandidate.LastName, Is.EqualTo(candidateDto.LastName));
            Assert.That(savedCandidate.PhoneNumber, Is.EqualTo(candidateDto.PhoneNumber));
            Assert.That(savedCandidate.PrefferedCallTime, Is.EqualTo(candidateDto.TimeToCall));
            Assert.That(savedCandidate.LinkedinProfileURL, Is.EqualTo(candidateDto.LinkedInUrl));
            Assert.That(savedCandidate.GitHubProfileURL, Is.EqualTo(candidateDto.GitHubUrl));
            Assert.That(savedCandidate.Comments, Is.EqualTo(candidateDto.Comments));

        }

        [Test]
        public async Task CreateOrUpdateCandidateAsync_ShouldUpdateExistingCandidate_WhenExists()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                Email = "test1@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                PrefferedCallTime = "2 PM - 3 PM",
                LinkedinProfileURL = "http://linkedin.com/in/test",
                GitHubProfileURL = "http://github.com/test",
                Comments = "Updated Test comment"
            };

            await _repository.UpdateCandidateAsync(existingCandidate);


            var candidateDto = new CandidateDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                TimeToCall = "2 PM - 3 PM",
                LinkedInUrl = "http://linkedin.com/in/test",
                GitHubUrl = "http://github.com/test",
                Comments = "Updated Test comment"
            };

            // Act
            await _service.CreateOrUpdateCandidateAsync(candidateDto);

            // Assert
            // Assert
            var updatedCandidate = await _repository.GetCandidateByEmailAsync(existingCandidate.Email);
            Assert.That(updatedCandidate.FirstName, Is.EqualTo(candidateDto.FirstName));
            Assert.That(updatedCandidate.LastName, Is.EqualTo(candidateDto.LastName));
            Assert.That(updatedCandidate.PhoneNumber, Is.EqualTo(candidateDto.PhoneNumber));
            Assert.That(updatedCandidate.PrefferedCallTime, Is.EqualTo(candidateDto.TimeToCall));
            Assert.That(updatedCandidate.LinkedinProfileURL, Is.EqualTo(candidateDto.LinkedInUrl));
            Assert.That(updatedCandidate.GitHubProfileURL, Is.EqualTo(candidateDto.GitHubUrl));
            Assert.That(updatedCandidate.Comments, Is.EqualTo(candidateDto.Comments));

        }


    }
}
