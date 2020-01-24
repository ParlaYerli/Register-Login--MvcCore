using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Register.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Register.DataContext
{
    public class ContextUser : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public ContextUser(DbContextOptions<ContextUser> dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=WISGNURUN113\SQLEXPRESS; Database=UserIdentityDB_; Trusted_Connection=True;");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(
        //        new User { Id = "1", UserName = "parla", Email = "parla@gmail.com" },
        //        new User { Id = "2", UserName = "ömer", Email = "ömer@gmail.com" },
        //        new User { Id = "3", UserName = "hazal", Email = "hazal@gmail.com" });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
