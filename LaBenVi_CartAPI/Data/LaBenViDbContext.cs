using LaBenVi_CartAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LaBenVi_CartAPI.Data
{
    public class LaBenViDbContext : DbContext
    {
        public LaBenViDbContext(DbContextOptions<LaBenViDbContext> options) : base(options)
        {
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
