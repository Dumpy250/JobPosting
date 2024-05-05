using System.Collections.Generic;

namespace JobPosting.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EmployerId { get; set; }

        public Employer Employer { get; set; }

        // Add this line
        public ICollection<ApplicantJob> ApplicantJobs { get; set; }
    }
}