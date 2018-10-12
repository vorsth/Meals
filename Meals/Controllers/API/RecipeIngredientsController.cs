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
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly MealsContext _context;

        public RecipeIngredientsController(MealsContext context)
        {
            _context = context;
        }

        // GET: api/RecipeIngredients
        [HttpGet]
        public IEnumerable<RecipeIngredient> GetRecipeIngredient()
        {
            return _context.RecipeIngredient;
        }

        // GET: api/RecipeIngredients/5
        [HttpGet("{recipeId}-{ingredientId}")]
        public async Task<IActionResult> GetRecipeIngredient([FromRoute] int recipeId, [FromRoute] int ingredientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeIngredient = await _context.RecipeIngredient.FindAsync(recipeId, ingredientId);

            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return Ok(recipeIngredient);
        }

        // PUT: api/RecipeIngredients/5
        [HttpPut("{recipeId}-{ingredientId}")]
        public async Task<IActionResult> PutRecipeIngredient([FromRoute] int recipeId, [FromRoute] int ingredientId, [FromBody] RecipeIngredient recipeIngredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (recipeId != recipeIngredient.RecipeId || ingredientId != recipeIngredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(recipeIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(recipeId, ingredientId))
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

        // POST: api/RecipeIngredients
        [HttpPost]
        public async Task<IActionResult> PostRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RecipeIngredient.Add(recipeIngredient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecipeIngredientExists(recipeIngredient.RecipeId, recipeIngredient.IngredientId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecipeIngredient", new { id = recipeIngredient.RecipeId }, recipeIngredient);
        }

        // DELETE: api/RecipeIngredients/5
        [HttpDelete("{recipeId}-{ingredientId}")]
        public async Task<IActionResult> DeleteRecipeIngredient([FromRoute] int recipeId, [FromRoute] int ingredientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeIngredient = await _context.RecipeIngredient.FindAsync(recipeId, ingredientId);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            _context.RecipeIngredient.Remove(recipeIngredient);
            await _context.SaveChangesAsync();

            return Ok(recipeIngredient);
        }

        private bool RecipeIngredientExists(int recipeId, int ingredientId)
        {
            return _context.RecipeIngredient.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId);
        }
    }
}