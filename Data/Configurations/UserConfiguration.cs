using exomine.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exomine.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var adminUser = new User
            {
                Id = 1,
                Username = "core",
                PasswordHash = "$2a$11$WkApcDx7b7xYmPEc77bwKuqClTsVbokRM1huCojjW40k8SKmqvfJy",
                Role = "Admin"
            };

            builder.HasData(adminUser);
        }
    }
}
