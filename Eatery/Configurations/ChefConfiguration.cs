using Eatery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eatery.Configurations
{
    public class ChefConfiguration : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Category).WithMany(x => x.Chefs).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
