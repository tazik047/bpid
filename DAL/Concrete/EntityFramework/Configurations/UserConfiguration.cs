using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Concrete.EntityFramework.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            HasKey(x => x.Email);
            Property(x => x.ContentType).HasMaxLength(50).IsRequired();
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Surname).HasMaxLength(100).IsRequired();
        }
    }
}
