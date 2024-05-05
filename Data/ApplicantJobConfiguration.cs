using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobPosting.Models;

namespace JobPosting.Data
{
    public class ApplicantJobConfiguration : IEntityTypeConfiguration<ApplicantJob>
    {
        public void Configure(EntityTypeBuilder<ApplicantJob> builder)
        {
            builder.ToTable("applicantjobs");
            builder.HasKey(aj => new { aj.ApplicantId, aj.JobId });
            builder.HasOne(aj => aj.Applicant)
            .WithMany(a => a.ApplicantJobs)
            .HasForeignKey(aj => aj.ApplicantId);
            builder.HasOne(aj => aj.Job)
            .WithMany(j => j.ApplicantJobs)
            .HasForeignKey(aj => aj.JobId);
        }
    }
}