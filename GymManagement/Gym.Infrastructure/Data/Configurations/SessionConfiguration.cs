using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Date)
               .HasColumnType("date");

        builder.Property(x => x.StartTime)
               .HasColumnType("time");

        builder.Property(x => x.Capacity)
               .IsRequired();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasOne(x => x.Trainer)
               .WithMany(x => x.Sessions)
               .HasForeignKey(x => x.TrainerId);

        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.Session)
               .HasForeignKey(x => x.SessionId);
    }
}