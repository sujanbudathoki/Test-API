using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace test_API.Domain.Entities
{
    public class Candidate
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone(ErrorMessage ="Invalid Phone Number")]
        public string? PhoneNumber { get; set; }
        [Key]
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
        public string? PrefferedCallTime { get; set; }
        public string? LinkedinProfileURL { get; set; }
        public string? GitHubProfileURL { get; set; }
        [Required]
        public string Comments { get; set; }

    }
}
