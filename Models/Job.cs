using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPosting.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Employer")]
        public int EmployerId { get; set; }

        public Employer? Employer { get; set; }

        public ICollection<ApplicantJob> ApplicantJobs { get; set; } = new List<ApplicantJob>();
    }
}
