using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using LaBenVi_AuthService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
