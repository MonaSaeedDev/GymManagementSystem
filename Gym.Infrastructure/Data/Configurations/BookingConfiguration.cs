using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(b => b.MemberId)
               .IsRequired();

        builder.Property(b => b.SessionId)
               .IsRequired();

        builder.HasOne(b => b.Member)
               .WithMany(m => m.Bookings)
               .HasForeignKey(b => b.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Session)
               .WithMany(s => s.Bookings)
               .HasForeignKey(b => b.SessionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.MemberId, b.SessionId })
               .IsUnique();
    }
}