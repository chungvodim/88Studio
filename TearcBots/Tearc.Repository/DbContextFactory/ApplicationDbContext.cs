using Microsoft.AspNet.Identity.EntityFramework;
using Tearc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Tearc.Data.Entity;

namespace Tearc.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=TearcDB")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Advert>().ToTable("Advert");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<ProductType>().ToTable("ProductType");
            modelBuilder.Entity<Source>().ToTable("Source");
        }
    }
}
