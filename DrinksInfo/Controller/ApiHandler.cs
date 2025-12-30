using DrinksInfo.Model;
using DrinksInfo.View;
using Spectre.Console;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace DrinksInfo.Controller
{
    internal class ApiHandler
    {
        internal async Task<List<string>> GetCategories(HttpClient client)
        {
            string url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";
            var response = await client.GetFromJsonAsync<DrinksCategories>(url);
            List<string> AllCategories = new();

            AllCategories.Add("Favorites");
            AllCategories.Add("Exit");

            if (response == null)
            {
                AnsiConsole.MarkupLine("[bold red] No response from Drinks API[/]");
                return AllCategories;
            }
            foreach (var category in response.drinks)
            {
                AllCategories.Add(category.strCategory);
            }
            return AllCategories;
        }

        internal async Task<List<string>> GetDrinksFromCategory(HttpClient client, string category)
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
            }

            return DrinksInCategory;
        }

        internal async Task<Drink> GetDrinkDetails(HttpClient client, string DrinkInfo)
        {
            DatabaseController.IncrementViews(DrinkInfo);
            var url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={DrinkInfo}";
            List<Drink> initiator = new();
            DrinksResponse response = new(initiator);

            try
            {
                response = await client.GetFromJsonAsync<DrinksResponse>(url);
                if (response == null)
                {
                    AnsiConsole.MarkupLine("[bold red] No response from Drinks API, returning to Main Menu[/]");
                    Console.ReadKey();
                    UserInterface.MainMenu();
                    return null;
                }
                if (response.drinks == null)
                {
                    AnsiConsole.MarkupLine($"[bold red] DrinksAPI Returned Null for '{DrinkInfo}', returning to Main Menu[/]");
                    Console.ReadKey();
                    UserInterface.MainMenu();
                    return null;
                }
            }
            catch (JsonException ex)
            {
                AnsiConsole.MarkupLine($"[red]Error deserializing JSON, please try again. \nError Message:[/][yellow]{ex}[/]");
                UserInterface.MainMenu();
            }
            var drink = response.drinks.ElementAt(0);
            return drink;
        }
        internal static void ShowImage(string ImageUrl)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = ImageUrl,
                    UseShellExecute = true //Lets windows choose the default browser
                });
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]Couldnt find Image, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
    }
}
