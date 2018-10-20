
TRUNCATE meals.Store;
SELECT SETVAL('meals.store_id_seq', 1);
INSERT INTO meals.Store (name) VALUES
	('Trader Joe''s'),
	('Safeway'),
	('Whole Foods');

TRUNCATE meals.Ingredient;
SELECT SETVAL('meals.ingredient_id_seq', 1);
INSERT INTO meals.Ingredient (name, storeid) VALUES
	('Spaghetti'),
	('Shredded Mozzarella Cheese'),
	('Sour Cream'),
	('Frozen Chopped Spinach'),
	('Egg'),
	('Garlic Salt'),
	('Spaghetti Sauce');

TRUNCATE meals.Unit;
SELECT SETVAL('meals.unit_id_seq', 1);
INSERT INTO meals.Unit (name, baseunitid) VALUES
	('Teaspoon (tsp)', NULL),
	('Cup', NULL),
	('Single', NULL),
	('Ounce (Oz)', NULL);

TRUNCATE meals.Recipe;
SELECT SETVAL('meals.recipe_id_seq', 1);
INSERT INTO meals.Recipe (name, description, servings, servingsours) VALUES
	('Spaghetti Casserole', 'YUM!', 6, 6);

TRUNCATE meals.RecipeIngredient;
INSERT INTO meals.RecipeIngredient (recipeid, ingredientid, quantity, unitid) VALUES
(2, 2, 8, 5),
(2, 3, 8, 5),
(2, 4, 1.5, 3),
(2, 5, 10, 5),
(2, 6, 1, 4),
(2, 7, 1, 2),
(2, 8, 2, 3);

TRUNCATE TABLE meals.Meal;
SELECT setval('meals.meal_id_seq',1);

INSERT INTO meals.Meal (name) VALUES
('Spaghetti Casserole');

TRUNCATE TABLE meals.MealRecipe;
INSERT INTO meals.MealRecipe (mealid, recipeid) VALUES
(2,2);

TRUNCATE TABLE meals.IngredientStore;
INSERT INTO meals.IngredientStore (ingredientid, storeid, priority) VALUES
(2,3,1),
(3,3,1),
(4,3,1),
(5,3,1),s
(6,3,1),
(7,3,1),
(8,3,1),


SELECT
	recipe.name,
	ri.quantity,
	unit.name,
	ingredient.name,
	store.name
FROM meals.RecipeIngredient ri
JOIN meals.Recipe recipe ON recipe.Id = ri.recipeid
JOIN meals.Ingredient ingredient ON ingredient.Id = ri.ingredientid
JOIN meals.Unit unit ON unit.Id = ri.unitid
JOIN meals.Store store ON store.id = ingredient.storeid;
