using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Capacity)
            .IsRequired();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.StartTime)
            .IsRequired();

        builder.Property(s => s.TraineerId)
            .IsRequired();

        builder.HasOne(s => s.Traineer)
            .WithMany(t => t.Sessions)
            .HasForeignKey(s => s.TraineerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}