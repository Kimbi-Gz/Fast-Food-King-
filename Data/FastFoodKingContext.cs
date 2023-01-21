using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FastFoodKing.Models;

namespace FastFoodKing.Data
{
    public class FastFoodKingContext : DbContext
    {
        public FastFoodKingContext (DbContextOptions<FastFoodKingContext> options)
            : base(options)
        {
        }

        public DbSet<FastFoodKing.Models.Category> Category { get; set; } = default!;

        public DbSet<FastFoodKing.Models.Menu> Menu { get; set; }

        public DbSet<FastFoodKing.Models.User> User { get; set; }

        public DbSet<FastFoodKing.Models.OrdenDetail> OrdenDetail { get; set; }

        public DbSet<FastFoodKing.Models.Cart> Cart { get; set; }
    }
}
