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
    public class ShoppingListRecipesController : ControllerBase
    {
        private readonly MealsContext _context;

        public ShoppingListRecipesController(MealsContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingListRecipes
        [HttpGet]
        public IEnumerable<ShoppingListRecipe> GetShoppingListRecipe()
        {
            return _context.ShoppingListRecipe;
        }

        // GET: api/ShoppingListRecipes/5
        [HttpGet("{shoppingListId}-{recipeId}")]
        public async Task<IActionResult> GetShoppingListRecipe([FromRoute] int shoppingListId, [FromRoute] int recipeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingListRecipe = await _context.ShoppingListRecipe.FindAsync(shoppingListId, recipeId);

            if (shoppingListRecipe == null)
            {
                return NotFound();
            }

            return Ok(shoppingListRecipe);
        }

        // PUT: api/ShoppingListRecipes/5
        [HttpPut("{shoppingListId}-{recipeId}")]
        public async Task<IActionResult> PutShoppingListRecipe([FromRoute] int shoppingListId, [FromRoute] int recipeId, [FromBody] ShoppingListRecipe shoppingListRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (shoppingListId != shoppingListRecipe.ShoppingListId || recipeId != shoppingListRecipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingListRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListRecipeExists(shoppingListId, recipeId))
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

        // POST: api/ShoppingListRecipes
        [HttpPost]
        public async Task<IActionResult> PostShoppingListRecipe([FromBody] ShoppingListRecipe shoppingListRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ShoppingListRecipe.Add(shoppingListRecipe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingListRecipeExists(shoppingListRecipe.ShoppingListId, shoppingListRecipe.RecipeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingListRecipe", new { shoppingListId = shoppingListRecipe.ShoppingListId, recipeId = shoppingListRecipe.RecipeId }, shoppingListRecipe);
        }

        // DELETE: api/ShoppingListRecipes/5
        [HttpDelete("{shoppingListId}-{recipeId}")]
        public async Task<IActionResult> DeleteShoppingListRecipe([FromRoute] int shoppingListId, [FromBody] int recipeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingListRecipe = await _context.ShoppingListRecipe.FindAsync(shoppingListId, recipeId);
            if (shoppingListRecipe == null)
            {
                return NotFound();
            }

            _context.ShoppingListRecipe.Remove(shoppingListRecipe);
            await _context.SaveChangesAsync();

            return Ok(shoppingListRecipe);
        }

        private bool ShoppingListRecipeExists(int shoppingListId, int recipeId)
        {
            return _context.ShoppingListRecipe.Any(e => e.ShoppingListId == shoppingListId && e.RecipeId == recipeId);
        }
    }
}