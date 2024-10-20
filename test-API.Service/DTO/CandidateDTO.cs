using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_API.Domain.DTO
{
    public class CandidateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Key]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public string TimeToCall { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
