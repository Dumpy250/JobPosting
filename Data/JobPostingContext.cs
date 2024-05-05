using Microsoft.EntityFrameworkCore;
using JobPosting.Models;

namespace JobPosting.Data
{
    public class JobPostingContext : DbContext
    {
        public JobPostingContext(DbContextOptions<JobPostingContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<ApplicantJob> ApplicantJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
            modelBuilder.ApplyConfiguration(new EmployerConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicantJobConfiguration());
        }
    }
}