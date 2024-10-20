using test_API.Domain.DTO;
using test_API.Domain.Entities;
using test_API.Service.IServices;

public class CandidateServiceStub : ICandidateService
{
    private readonly List<CandidateDto> _candidates = new List<CandidateDto>();

    public Task CreateOrUpdateCandidateAsync(CandidateDto candidateDto)
    {
        var existingCandidate = _candidates.FirstOrDefault(c => c.Email == candidateDto.Email);
        if (existingCandidate == null)
        {
            _candidates.Add(candidateDto);
        }
        else
        {
            existingCandidate.FirstName = candidateDto.FirstName;
            existingCandidate.LastName = candidateDto.LastName;
            existingCandidate.PhoneNumber = candidateDto.PhoneNumber;
            existingCandidate.TimeToCall = candidateDto.TimeToCall;
            existingCandidate.LinkedInUrl = candidateDto.LinkedInUrl;
            existingCandidate.GitHubUrl = candidateDto.GitHubUrl;
            existingCandidate.Comments = candidateDto.Comments;
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Candidate?>> GetAllCandidatesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CandidateDto> GetCandidateByEmailAsync(string email)
    {
        var candidate = _candidates.FirstOrDefault(c => c.Email == email);
        return Task.FromResult(candidate);
    }
}