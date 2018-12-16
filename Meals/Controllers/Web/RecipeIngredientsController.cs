using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meals.Models;

namespace Meals.Controllers.Web
{
    [Route("[controller]")]
    public class RecipeIngredientsController : Controller
    {
        private readonly MealsContext _context;

        public RecipeIngredientsController(MealsContext context)
        {
            _context = context;
        }

        // GET: RecipeIngredients
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mealsContext = _context.RecipeIngredient
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .Include(r => r.Unit);
            return View(await mealsContext.ToListAsync());
        }

        // GET: RecipeIngredients/Details/5
        [HttpGet("Details/{recipeId}/{ingredientId}")]
        public async Task<IActionResult> Details(int? recipeId, int? ingredientId)
        {
            if (recipeId == null || ingredientId == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredient
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(m => m.RecipeId == recipeId && m.IngredientId == ingredientId);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, nameof(Ingredient.Id), nameof(Ingredient.Name));
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name));
            ViewData["UnitId"] = new SelectList(_context.Unit, nameof(Unit.Id), nameof(Unit.Name));
            return View();
        }

        // POST: RecipeIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,IngredientId,Quantity,UnitId")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, nameof(Ingredient.Id), nameof(Ingredient.Name), recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), recipeIngredient.RecipeId);
            ViewData["UnitId"] = new SelectList(_context.Unit, nameof(Unit.Id), nameof(Unit.Name), recipeIngredient.UnitId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        [HttpGet("Edit/{recipeId}/{ingredientId}")]
        public async Task<IActionResult> Edit(int? recipeId, int? ingredientId)
        {
            if (recipeId == null || ingredientId == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredient
                .Include(r => r.Recipe)
                .Include(r => r.Ingredient)
                .FirstOrDefaultAsync(m => m.RecipeId == recipeId && m.IngredientId == ingredientId);
            if (recipeIngredient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, nameof(Ingredient.Id), nameof(Ingredient.Name), recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), recipeIngredient.RecipeId);
            ViewData["UnitId"] = new SelectList(_context.Unit, nameof(Unit.Id), nameof(Unit.Name), recipeIngredient.UnitId);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{recipeId}/{ingredientId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int recipeId, int ingredientId, [Bind("RecipeId,IngredientId,Quantity,UnitId")] RecipeIngredient recipeIngredient)
        {
            if (recipeId != recipeIngredient.RecipeId || ingredientId != recipeIngredient.IngredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeIngredientExists(recipeIngredient.RecipeId, recipeIngredient.IngredientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, nameof(Ingredient.Id), nameof(Ingredient.Name), recipeIngredient.IngredientId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), recipeIngredient.RecipeId);
            ViewData["UnitId"] = new SelectList(_context.Unit, nameof(Unit.Id), nameof(Unit.Name), recipeIngredient.UnitId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        [HttpGet("Delete/{recipeId}/{ingredientId}")]
        public async Task<IActionResult> Delete(int? recipeId, int? ingredientId)
        {
            if (recipeId == null || ingredientId == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredient
                .Include(r => r.Ingredient)
                .Include(r => r.Recipe)
                .Include(r => r.Unit)
                .FirstOrDefaultAsync(m => m.RecipeId == recipeId && ingredientId == m.IngredientId);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete/5
        [HttpPost("Delete/{recipeId}/{ingredientId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int recipeId, int ingredientId)
        {
            var recipeIngredient = await _context.RecipeIngredient.FindAsync(recipeId, ingredientId);
            _context.RecipeIngredient.Remove(recipeIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeIngredientExists(int recipeId, int ingredientId)
        {
            return _context.RecipeIngredient.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId);
        }
    }
}
