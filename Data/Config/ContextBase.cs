using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase()
        { }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        { }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<UpExcel> UpExcels { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=ApiTestes;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.6.41-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            builder.Entity<UpExcel>().ToTable("UpExcel").HasKey(t => t.ExcelId);
            base.OnModelCreating(builder);
        }
    }
}

