using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_API.Domain.Entities
{
    public class Candidates
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        [Key]
        [Required]
        public string Email { get; set; }
        public string? PrefferedCallTime { get; set; }
        public string? LinkedinProfileURL { get; set; }
        public string? GitHubProfileURL { get; set; }
        [Required]
        public string Comments { get; set; }

    }
}
