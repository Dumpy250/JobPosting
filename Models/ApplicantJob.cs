using System.ComponentModel.DataAnnotations;

namespace JobPosting.Models
{
    public class ApplicantJob
    {
        [Required]
        public int ApplicantId { get; set; }

        [Required]
        public int JobId { get; set; }

        // Navigation properties
        public Applicant Applicant { get; set; }
        public Job Job { get; set; }
    }
}