using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete.EntityFramework.Configurations;
using Models;

namespace DAL.Concrete.EntityFramework
{
    internal class DbContext : System.Data.Entity.DbContext
    {
        static DbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbContext>());
        }

        public DbContext()
            :base ("defaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            Entry(entity).State = state;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
        }
    }
}
