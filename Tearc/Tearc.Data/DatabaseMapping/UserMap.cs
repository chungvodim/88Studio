using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tearc.Data
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<ApplicationUser > entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.Property(t => t.PasswordHash).IsRequired();
            entityBuilder.Property(t => t.Email).IsRequired();
        }
    }
}
