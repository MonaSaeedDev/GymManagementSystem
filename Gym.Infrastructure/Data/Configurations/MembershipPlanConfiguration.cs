using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Data.Configurations;

public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        builder.ToTable("MembershipPlans");

        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mp => mp.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(mp => mp.MaxSessionsPerMonth)
            .IsRequired();
    }
}