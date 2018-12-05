using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Meals.Models;

namespace Meals.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealRecipesController : ControllerBase
    {
        private readonly MealsContext _context;

        public MealRecipesController(MealsContext context)
        {
            _context = context;
        }

        // GET: api/MealRecipes
        [HttpGet]
        public IEnumerable<MealRecipe> GetMealRecipe()
        {
            return _context.MealRecipe;
        }

        // GET: api/MealRecipes/5
        [HttpGet("{mealId}-{recipeId}")]
        public async Task<IActionResult> GetMealRecipe([FromRoute] int mealId, [FromRoute] int recipeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mealRecipe = await _context.MealRecipe.FindAsync(mealId, recipeId);

            if (mealRecipe == null)
            {
                return NotFound();
            }

            return Ok(mealRecipe);
        }

        // PUT: api/MealRecipes/5
        [HttpPut("{mealId}-{recipeId}")]
        public async Task<IActionResult> PutMealRecipe([FromRoute] int mealId, [FromRoute] int recipeId, [FromBody] MealRecipe mealRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (mealId != mealRecipe.MealId || recipeId != mealRecipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(mealRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealRecipeExists(mealId, recipeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MealRecipes
        [HttpPost]
        public async Task<IActionResult> PostMealRecipe([FromBody] MealRecipe mealRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MealRecipe.Add(mealRecipe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MealRecipeExists(mealRecipe.MealId, mealRecipe.RecipeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMealRecipe", new { mealId = mealRecipe.MealId, recipeId = mealRecipe.RecipeId }, mealRecipe);
        }

        // DELETE: api/MealRecipes/5
        [HttpDelete("{mealId}-{recipeId}")]
        public async Task<IActionResult> DeleteMealRecipe([FromRoute] int mealId, [FromBody] int recipeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mealRecipe = await _context.MealRecipe.FindAsync(mealId, recipeId);
            if (mealRecipe == null)
            {
                return NotFound();
            }

            _context.MealRecipe.Remove(mealRecipe);
            await _context.SaveChangesAsync();

            return Ok(mealRecipe);
        }

        private bool MealRecipeExists(int mealId, int recipeId)
        {
            return _context.MealRecipe.Any(e => e.MealId == mealId && e.RecipeId == recipeId);
        }
    }
}