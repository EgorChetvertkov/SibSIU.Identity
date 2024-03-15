using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SibSIU.UserData.Database.Entities;

namespace SibSIU.Auth.Database.Entities.Configuration;
public sealed class TempStudentDeanCodeFromDeanDBConfiguration
    : IEntityTypeConfiguration<TempStudentDeanCodeFromDeanDB>
{
    public void Configure(EntityTypeBuilder<TempStudentDeanCodeFromDeanDB> builder)
    {
        builder.HasKey(s => s.DeanCode);
        
        builder.Property(s => s.DeanCode).IsRequired().ValueGeneratedNever();
        builder.Property(s => s.FirstName).IsRequired();
        builder.Property(s => s.LastName).IsRequired();
        builder.Property(s => s.GroupName).IsRequired();
        builder.Property(s => s.Rank).IsRequired();
        builder.Property(s => s.Birthday).IsRequired();
    }
}
