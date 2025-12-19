using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Specialty)
            .IsRequired()
            .HasMaxLength(100);
    }
}