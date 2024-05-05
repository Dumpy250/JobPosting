using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobPosting.Models;

namespace JobPosting.Data
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("jobs");

            // Configure properties
            builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(500);

            // Configure relationships
            builder.HasOne(j => j.Employer)
            .WithMany(e => e.Jobs)
            .HasForeignKey(j => j.EmployerId);
        }
    }
}