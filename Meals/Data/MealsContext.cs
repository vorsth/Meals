using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Meals.Models;

namespace Meals.Models
{
    public class MealsContext : DbContext
    {
        public MealsContext (DbContextOptions<MealsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => new
            {
                ri.RecipeId,
                ri.IngredientId
            });

            modelBuilder.Entity<MealRecipe>().HasKey(rm => new
            {
                rm.MealId,
                rm.RecipeId
            });
        }

        public DbSet<Meals.Models.Ingredient> Ingredient { get; set; }

        public DbSet<Meals.Models.Store> Store { get; set; }

        public DbSet<Meals.Models.Unit> Unit { get; set; }

        public DbSet<Meals.Models.ShoppingList> ShoppingList { get; set; }

        public DbSet<Meals.Models.RecipeIngredient> RecipeIngredient { get; set; }

        public DbSet<Meals.Models.MealRecipe> MealRecipe { get; set; }

        public DbSet<Meals.Models.Recipe> Recipe { get; set; }
    }
}
