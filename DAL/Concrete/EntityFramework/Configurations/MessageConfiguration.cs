using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Concrete.EntityFramework.Configurations
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            ToTable("Messages");
            HasKey(x => x.Id);
            Property(x => x.Text).HasMaxLength(1000).IsRequired();
            HasRequired(x => x.From).WithMany(x => x.OutcomingMessages).HasForeignKey(x => x.FromId).WillCascadeOnDelete(false);
            HasRequired(x => x.To).WithMany(x => x.IncomingMessages).HasForeignKey(x => x.ToId).WillCascadeOnDelete(false);
        }
    }
}
