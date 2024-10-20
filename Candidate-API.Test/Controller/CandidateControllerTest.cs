using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using test_API.Domain.DTO;
using test_API.API.Controllers;
using test_API.Service.Services;
using test_API.Service.IServices;
using test_API.Domain.Entities;





[TestFixture]
public class CandidateControllerTests
{
    private CandidatesController _candidateController;
    private CandidateServiceStub _candidateServiceStub;

    [SetUp]
    public void Setup()
    {
        _candidateServiceStub = new CandidateServiceStub();
        _candidateController = new CandidatesController(_candidateServiceStub);
    }

    [Test]
    public async Task CreateCandidate_ReturnsCreatedAtAction_WhenCandidateIsValid()
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
        var result = await _candidateController.CreateOrUpdate(candidateDto);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result); // Check if the result is of type CreatedAtActionResult

        var createdAtActionResult = result as CreatedAtActionResult; // Cast to CreatedAtActionResult

        Assert.AreEqual("CreateOrUpdate", createdAtActionResult.ActionName); // Check the action name
        Assert.AreEqual(candidateDto, createdAtActionResult.Value); // Check the value of the result
    }

    [Test]
    public async Task CreateCandidate_ReturnsBadRequest_WhenCandidateDtoIsNull()
    {
        // Act
        var result = await _candidateController.CreateOrUpdate(null);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
   
    public async Task GetCandidateByEmail_ReturnsOk_WhenCandidateExists()
    {
        // Arrange
        var email = "test@example.com";
        var candidateDto = new CandidateDto
        {
            Email = email,
            FirstName = "John",
            LastName = "Doe"
        };

        await _candidateServiceStub.CreateOrUpdateCandidateAsync(candidateDto);

        // Act
        var result = await _candidateController.GetByEmail(email);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result); 
        var okResult = result as OkObjectResult; 

        Assert.AreEqual(candidateDto, okResult.Value); 
    }


    [Test]
    public async Task GetCandidateByEmail_ReturnsNotFound_WhenCandidateDoesNotExist()
    {
        // Arrange
        var email = "nonexistent@example.com";

        // Act
        var result = await _candidateController.GetByEmail(email);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}
