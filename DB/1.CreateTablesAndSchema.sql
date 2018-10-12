CREATE SCHEMA meals

CREATE TABLE meals.Ingredient (
	Id      serial       CONSTRAINT PK_Ingredient PRIMARY KEY,
	Name    varchar(255) NOT NULL,
	StoreId int
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
	BaseUnitId  int          NULL,
	Multiplier  real       NULL
)

CREATE TABLE meals.RecipeIngredient (
	RecipeId    int NOT NULL,
	IngredientId int NOT NULL,
	Quantity     int NOT NULL,
	UnitId       int NOT NULL,
	CONSTRAINT PK_RecipeIngredient PRIMARY KEY (RecipeID, IngredientID)
)

CREATE TABLE meals.Meal (
	Id   serial       CONSTRAINT PK_Meal PRIMARY KEY,
	Name varchar(255) NOT NULL
)

CREATE TABLE meals.MealRecipe (
    MealId   int NOT NULL,
    RecipeId int NOT NULL,
    CONSTRAINT PK_RecipeMeal PRIMARY KEY (MealId, RecipeId)
)
