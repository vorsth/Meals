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
    public class ShoppingListRecipesController : Controller
    {
        private readonly MealsContext _context;

        public ShoppingListRecipesController(MealsContext context)
        {
            _context = context;
        }

        // GET: ShoppingListRecipes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shoppingListRecipeContext = _context.ShoppingListRecipe
                .Include(s => s.ShoppingList)
                .Include(s => s.Recipe);
            return View(await shoppingListRecipeContext.ToListAsync());
        }

        // GET: ShoppingListRecipes/Details/5
        [HttpGet("Details/{shoppingListId}/{recipeId}")]
        public async Task<IActionResult> Details(int? shoppingListId, int? recipeId)
        {
            if (shoppingListId == null || recipeId == null)
            {
                return NotFound();
            }

            var shoppingListRecipe = await _context.ShoppingListRecipe
                .Include(s => s.ShoppingList)
                .Include(s => s.Recipe)
                .FirstOrDefaultAsync(m => m.ShoppingListId == shoppingListId && m.RecipeId == recipeId);
            if (shoppingListRecipe == null)
            {
                return NotFound();
            }

            return View(shoppingListRecipe);
        }

        // GET: ShoppingListRecipes/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingList, nameof(ShoppingList.Id), nameof(ShoppingList.Name));
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name));
            return View();
        }

        // POST: ShoppingListRecipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingListId,RecipeId,Quantity")] ShoppingListRecipe shoppingListRecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingListRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingList, nameof(ShoppingList.Id), nameof(ShoppingList.Name), shoppingListRecipe.ShoppingListId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), shoppingListRecipe.RecipeId);
            return View(shoppingListRecipe);
        }

        // GET: ShoppingListRecipes/Edit/5
        [HttpGet("Edit/{shoppingListId}/{recipeId}")]
        public async Task<IActionResult> Edit(int? shoppingListId, int? recipeId)
        {
            if (shoppingListId == null || recipeId == null)
            {
                return NotFound();
            }

            var shoppingListRecipe = await _context.ShoppingListRecipe
                .Include(s => s.ShoppingList)
                .Include(s => s.Recipe)
                .FirstOrDefaultAsync(m => m.ShoppingListId == shoppingListId && m.RecipeId == recipeId);
            if (shoppingListRecipe == null)
            {
                return NotFound();
            }
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingList, nameof(ShoppingList.Id), nameof(ShoppingList.Name), shoppingListRecipe.ShoppingListId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), shoppingListRecipe.RecipeId);
            return View(shoppingListRecipe);
        }

        // POST: ShoppingListRecipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{shoppingListId}/{recipeId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int shoppingListId, int recipeId, [Bind("ShoppingListId,RecipeId,Quantity")] ShoppingListRecipe shoppingListRecipe)
        {
            if (shoppingListId != shoppingListRecipe.ShoppingListId || recipeId != shoppingListRecipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingListRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListRecipeExists(shoppingListRecipe.ShoppingListId, shoppingListRecipe.RecipeId))
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
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingList, nameof(ShoppingList.Id), nameof(ShoppingList.Name), shoppingListRecipe.ShoppingListId);
            ViewData["RecipeId"] = new SelectList(_context.Recipe, nameof(Recipe.Id), nameof(Recipe.Name), shoppingListRecipe.RecipeId);
            return View(shoppingListRecipe);
        }

        // GET: ShoppingListRecipes/Delete/5
        [HttpGet("Delete/{shoppingListId}/{recipeId}")]
        public async Task<IActionResult> Delete(int? shoppingListId, int? recipeId)
        {
            if (shoppingListId == null || recipeId == null)
            {
                return NotFound();
            }

            var shoppingListRecipe = await _context.ShoppingListRecipe
                .Include(s => s.ShoppingList)
                .Include(s => s.Recipe)
                .FirstOrDefaultAsync(m => m.ShoppingListId == shoppingListId && m.RecipeId == recipeId);
            if (shoppingListRecipe == null)
            {
                return NotFound();
            }

            return View(shoppingListRecipe);
        }

        // POST: ShoppingListRecipes/Delete/5
        [HttpPost("Delete/{shoppingListId}/{recipeId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int shoppingListId, int recipeId)
        {
            var shoppingListRecipe = await _context.ShoppingListRecipe.FindAsync(shoppingListId, recipeId);
            _context.ShoppingListRecipe.Remove(shoppingListRecipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingListRecipeExists(int shoppingListId, int recipeId)
        {
            return _context.ShoppingListRecipe.Any(e => e.ShoppingListId == shoppingListId && e.RecipeId == recipeId);
        }
    }
}
