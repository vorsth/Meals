using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meals.Models;
using Meals.ViewModels;

namespace Meals.Controllers.Web
{
    [Route("[controller]")]
    public class ShoppingListsController : Controller
    {
        private readonly MealsContext _context;

        public ShoppingListsController(MealsContext context)
        {
            _context = context;
        }

        // GET: ShoppingLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingList.ToListAsync());
        }

        // GET: ShoppingLists/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return View(shoppingList);
        }

        // GET: ShoppingLists/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ShoppingList shoppingList)
        {
            shoppingList.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(shoppingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingList);
        }

        // GET: ShoppingLists/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingList.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }
            return View(shoppingList);
        }

        // POST: ShoppingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ShoppingList shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return NotFound();
            }
            // Get the original shopping list so we can maintain the CreationDate 
            var originalShoppingList = await _context.ShoppingList.FindAsync(shoppingList.Id);
            if(originalShoppingList == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    originalShoppingList.Name = shoppingList.Name;

                    _context.Update(originalShoppingList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListExists(shoppingList.Id))
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
            return View(originalShoppingList);
        }

        // GET: ShoppingLists/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return View(shoppingList);
        }

        // POST: ShoppingLists/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingList = await _context.ShoppingList.FindAsync(id);
            _context.ShoppingList.Remove(shoppingList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Shop/{shoppingListId}")]
        public async Task<IActionResult> Shop(int? shoppingListId)
        {
            if (shoppingListId == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingList
                .FirstOrDefaultAsync(s => s.Id == shoppingListId);
            if(shoppingList == null)
            {
                return NotFound();
            }

            var ingredientsForList = await (from shoppinglist in _context.ShoppingList
                                            join shoppinglistrecipe in _context.ShoppingListRecipe on shoppinglist.Id equals shoppinglistrecipe.ShoppingListId
                                            join recipesingredient in _context.RecipeIngredient on shoppinglistrecipe.RecipeId equals recipesingredient.RecipeId
                                            join ingredient in _context.Ingredient on recipesingredient.IngredientId equals ingredient.Id
                                            join unit in _context.Unit on recipesingredient.UnitId equals unit.Id
                                            where shoppinglist.Id == shoppingListId
                                            select new ShoppingListIngredient(ingredient, recipesingredient.Quantity * shoppinglistrecipe.Quantity, unit))
                                      .ToListAsync();

            return View(new ShopVM(shoppingList, ingredientsForList));
        }

        private bool ShoppingListExists(int id)
        {
            return _context.ShoppingList.Any(e => e.Id == id);
        }
    }
}
