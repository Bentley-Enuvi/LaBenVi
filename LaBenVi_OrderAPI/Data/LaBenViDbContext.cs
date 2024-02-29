using LaBenVi_OrderAPI.Models;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace LaBenVi_OrderAPI.Data
{
    public class LaBenViDbContext : DbContext
    {
        public LaBenViDbContext(DbContextOptions<LaBenViDbContext> options) : base(options)
        {
        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
