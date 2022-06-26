using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CupcakeApi.Models
{
    public class CupcakeContext : DbContext
    {
        public CupcakeContext(DbContextOptions<CupcakeContext> options)
            : base(options)
        {
        }

        public DbSet<CupcakeItem> CupcakeItems { get; set; } = null!;
    }
}