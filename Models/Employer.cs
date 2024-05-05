using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPosting.Models
{
    public class Employer
    {
        public Employer()
        {
            Jobs = new List<Job>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Job> Jobs { get; set; }
    }
}