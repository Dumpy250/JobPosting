using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JobPosting.Models;

namespace JobPosting.Data
{
    public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("employers");
            builder.HasMany(e => e.Jobs)
            .WithOne(j => j.Employer)
            .HasForeignKey(j => j.EmployerId);
        }
    }
}