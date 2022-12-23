using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FastFoodKing.Models;
using FastFoodKing.Data;

namespace FastFoodKing.Data
{
    public class FastFoodKingContext : DbContext
    {

        public FastFoodKingContext(DbContextOptions<FastFoodKingContext> options): base(options)
        {
        }
        public DbSet<FastFoodKing.Models.Users> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<Users>().ToTable("User");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<OrdenDetails>().ToTable("OrdenDetails");

        }

        public DbSet<FastFoodKing.Models.Category> Category { get; set; }

        public DbSet<FastFoodKing.Models.Menu> Menu { get; set; }

        public DbSet<FastFoodKing.Models.Cart> Cart { get; set; }

        public DbSet<FastFoodKing.Models.OrdenDetails> OrdenDetails { get; set; }

    }

}



