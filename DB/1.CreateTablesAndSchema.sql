CREATE SCHEMA meals

CREATE TABLE meals.Ingredient (
	Id      serial       CONSTRAINT PK_Ingredient PRIMARY KEY,
	Name    varchar(255) NOT NULL
)

CREATE TABLE meals.Store (
	Id   serial       CONSTRAINT PK_Store PRIMARY KEY,
	Name varchar(255) NOT NULL
)

CREATE TABLE meals.Recipe (
	Id           serial       CONSTRAINT PK_Recipe PRIMARY KEY,
	Name         varchar(255) NOT NULL,
	Description  text         NULL,
	Servings     integer      NOT NULL,
	ServingsOurs integer      NULL
)

CREATE TABLE meals.Unit (
	Id          serial       CONSTRAINT PK_Unit PRIMARY KEY,
	Name        varchar(255) NOT NULL,
	BaseUnitId  int          NULL      REFERENCES meals.Unit(Id),
	Multiplier  real         NULL
)

CREATE TABLE meals.RecipeIngredient (
	RecipeId     int NOT NULL  REFERENCES meals.Recipe(Id),
	IngredientId int NOT NULL REFERENCES meals.Ingredient(Id),
	Quantity     int NOT NULL,
	UnitId       int NOT NULL REFERENCES meals.Unit(Id),
	CONSTRAINT PK_RecipeIngredient PRIMARY KEY (RecipeID, IngredientID)
)

CREATE TABLE meals.ShoppingList (
	Id           serial       CONSTRAINT PK_ShoppingList PRIMARY KEY,
	Name         varchar(255) NOT NULL,
	CreationDate timestamp    NOT NULL
)

CREATE TABLE meals.ShoppingListRecipe (
    ShoppingListId   int NOT NULL REFERENCES meals.ShoppingList(Id),
    RecipeId int NOT NULL REFERENCES meals.Recipe(Id),
    CONSTRAINT PK_ShoppingListRecipe PRIMARY KEY (ShoppingListId, RecipeId)
)

CREATE TABLE meals.IngredientStore (
    IngredientId   int NOT NULL REFERENCES meals.Ingredient(Id),
    StoreId int NOT NULL REFERENCES meals.Store(Id),
    Priority int NOT NULL,
    CONSTRAINT PK_IngredientStore PRIMARY KEY (IngredientId, StoreId)
)
