using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Phone)
            .IsRequired() 
            .HasMaxLength(100);

        builder.Property(m => m.MembershipStartDate)
            .IsRequired() 
            .HasColumnType("date");

        builder.Property(m => m.MembershipEndDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(m => m.Status)
            .IsRequired() 
            .HasConversion<string>();

        builder.HasOne(m => m.MembershipPlan)
            .WithMany()
            .HasForeignKey(m => m.MembershipPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}