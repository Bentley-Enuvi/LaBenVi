using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using LaBenVi_AuthService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace LaBenVi_AuthService.Data
{
    public class LaBenViDbContext : IdentityDbContext<AppUser>
    {
        public LaBenViDbContext(DbContextOptions<LaBenViDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<EmailLogger> EmailLoggers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<EmailLogger>(entity =>
            //{
            //    entity.ToTable("EmailLoggers"); // Specify the table name
            //    //entity.HasKey(e => e.Id); // Specify the primary key
            //    modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            //    modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            //    modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            //});
        }
    }
}
