using EmailAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailAPI.Data
{
    public class LaBenViDbContext : DbContext
    {
        public LaBenViDbContext(DbContextOptions<LaBenViDbContext> options) : base(options)
        {
        }

        
    }
}
