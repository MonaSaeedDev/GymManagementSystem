using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Phone)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.MembershipStartDate)
               .HasColumnType("date");

        builder.Property(x => x.MembershipEndDate)
               .HasColumnType("date");

        builder.Property(x => x.Status)
               .IsRequired();

        builder.HasOne(x => x.MembershipPlan)
               .WithMany()
               .HasForeignKey(x => x.MembershipPlanId);

        builder.HasMany(x => x.Bookings)
               .WithOne(x => x.Member)
               .HasForeignKey(x => x.MemberId);
    }
}
