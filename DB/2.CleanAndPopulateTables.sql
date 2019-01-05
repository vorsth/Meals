
TRUNCATE meals.Store CASCADE;
SELECT SETVAL('meals.store_id_seq', 1);
INSERT INTO meals.Store (name) VALUES
	('Trader Joe''s'),
	('Safeway'),
	('Whole Foods');

TRUNCATE meals.Ingredient CASCADE;
SELECT SETVAL('meals.ingredient_id_seq', 1);
INSERT INTO meals.Ingredient (name) VALUES
	('Spaghetti'),
	('Shredded Mozzarella Cheese'),
	('Sour Cream'),
	('Frozen Chopped Spinach'),
	('Egg'),
	('Garlic Salt'),
	('Spaghetti Sauce');

TRUNCATE meals.Unit CASCADE;
SELECT SETVAL('meals.unit_id_seq', 1);
INSERT INTO meals.Unit (name, baseunitid) VALUES
	('Teaspoon (tsp)', NULL),
	('Cup', NULL),
	('Single', NULL),
	('Ounce (Oz)', NULL);

TRUNCATE meals.Recipe CASCADE;
SELECT SETVAL('meals.recipe_id_seq', 1);
INSERT INTO meals.Recipe (name, description, servings, servingsours) VALUES
	('Spaghetti Casserole', 'YUM!', 6, 6);

TRUNCATE meals.RecipeIngredient CASCADE;
INSERT INTO meals.RecipeIngredient (recipeid, ingredientid, quantity, unitid) VALUES
(2, 2, 8, 5),
(2, 3, 8, 5),
(2, 4, 1.5, 3),
(2, 5, 10, 5),
(2, 6, 1, 4),
(2, 7, 1, 2),
(2, 8, 2, 3);

TRUNCATE TABLE meals.ShoppingList CASCADE;
SELECT setval('meals.shoppinglist_id_seq',1);

INSERT INTO meals.ShoppingList (name, creationDate) VALUES
('Week 1', now());

TRUNCATE TABLE meals.ShoppingListRecipe CASCADE;
INSERT INTO meals.ShoppingListRecipe (shoppinglistid, recipeid) VALUES
(2,2);

TRUNCATE TABLE meals.IngredientStore CASCADE;
INSERT INTO meals.IngredientStore (ingredientid, storeid, priority) VALUES
(2,3,1),
(3,3,1),
(4,3,1),
(5,3,1),
(6,3,2),
(6,2,1),
(7,3,1),
(8,3,1);

SELECT RecipeName, Quantity, Ingredient, Unit, Store FROM (
	SELECT
		recipe.name as RecipeName,
		ri.quantity as Quantity,
		ingredient.name as Ingredient,
		unit.name as Unit,
		store.name as Store,
		ingredientStore.priority as Priority,
		min(ingredientStore.priority) OVER (PARTITION BY recipe.name, ingredient.name) as MinPriority
	FROM meals.RecipeIngredient ri
	JOIN meals.Recipe recipe ON recipe.Id = ri.recipeid
	JOIN meals.Ingredient ingredient ON ingredient.Id = ri.ingredientid
	JOIN meals.Unit unit ON unit.Id = ri.unitid
	JOIN meals.IngredientStore ingredientStore ON ingredientStore.ingredientid = ingredient.Id
	JOIN meals.Store store ON store.id = ingredientStore.storeid
) AS RecipeIngredientNames
WHERE Priority = MinPriority;
