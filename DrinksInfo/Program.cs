using DrinksInfo;
using Spectre.Console;
using System.Net.Http.Json;

HttpClient client = new();
/*var categories = await GetCategories(client);
Console.ReadKey();
Console.Clear();
var drinks = await GetDrinksFromCategory(client, "Ordinary_Drink");
Console.ReadKey();
Console.Clear();*/
await GetDrinkDetailsById(client, "11007");
Console.ReadKey();


static async Task<List<string>> GetCategories(HttpClient client)
{
    string url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";
    var response = await client.GetFromJsonAsync<DrinksCategories>(url);
    List<string> AllCategories = new();

    if (response == null)
    {
        AnsiConsole.MarkupLine("[bold red] No response from Drinks API[/]");
        return AllCategories;
    }
    foreach (var category in response.drinks)
    {
        AllCategories.Add(category.strCategory);
        AnsiConsole.MarkupLine(category.strCategory);
    }
    return AllCategories;
}

static async Task<List<string>> GetDrinksFromCategory(HttpClient client, string category)
{
    List<string> DrinksInCategory = new();
    string url = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}";
    var response = await client.GetFromJsonAsync<DrinksResponse>(url);
    if (response == null)
    {
        AnsiConsole.MarkupLine("[bold red] No response from Drinks API[/]");
        return DrinksInCategory;
    }
    foreach (var drink in response.drinks)
    {
        DrinksInCategory.Add(drink.strDrink);
        AnsiConsole.Markup($"Drink: {drink.strDrink}, idDrink: {drink.idDrink}\n");
    }

    return DrinksInCategory;
}

static async Task GetDrinkDetailsById(HttpClient client, string DrinkId)
{
    string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={DrinkId}";
    var response = await client.GetFromJsonAsync<DrinksResponse>(url);
    if (response == null)
    {
        AnsiConsole.MarkupLine("[bold red] No response from Drinks API[/]");
        return;
    }
    var drink = response.drinks.ElementAt<Drink>(0);

    AnsiConsole.MarkupLine($"Drink: {drink.strDrink}\nInstructions: {drink.strInstructions}\n");

    var measuresList = drink.MakeMeasuresList();
    var ingredientsList = drink.MakeIngredientsList();
    string measure = "";
    for (int i = 0; i < ingredientsList.Count; i++)
    {
        if (i >= measuresList.Count) measure = "Up to you";
        else measure = measuresList.ElementAt(i);
        string ingredient = ingredientsList.ElementAt(i);
        AnsiConsole.MarkupLine($"Ingredient: {ingredient}, Measure: {measure}\n");
    }
    return;
}

static async Task GetDrinkDetailsByname(HttpClient client, string DrinkName)
{
    string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={DrinkName}";
}