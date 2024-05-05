using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPosting.Models
{
    public class Applicant
    {
        public Applicant()
        {
            ApplicantJobs = new List<ApplicantJob>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation properties
        public ICollection<ApplicantJob> ApplicantJobs { get; set; }
    }
}