using DrinksInfo.Controller;
using DrinksInfo.Model;
using Spectre.Console;

namespace DrinksInfo.View
{
    internal class UserInterface
    {
        internal static void MainMenu()
        {
            HttpClient client = new();
            ApiHandler handler = new();
            Console.Clear();
            var ChosenCategory = CategoriesMenu(client, handler);
            var DrinksInCategory = handler.GetDrinksFromCategory(client, ChosenCategory);

            var ChosenDrink = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[yellow bold]Which drink are you interested in?[/]")
                .EnableSearch()
                .SearchPlaceholderText("[gray]Type to search[/]")
                .AddChoices(DrinksInCategory.Result));

            var drink = handler.GetDrinkDetails(client, ChosenDrink);
            ShowDrinkDetail(drink.Result);
        }
        internal static string CategoriesMenu(HttpClient client, ApiHandler handler)
        {
            var AllCategories = handler.GetCategories(client);
            var ChosenCategory = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[yellow bold]Which Category are you interested in?[/]")
                .EnableSearch()
                .SearchPlaceholderText("[gray]Type to search[/]")
                .AddChoices(AllCategories.Result));

            if (ChosenCategory == "Exit")
            {
                AnsiConsole.MarkupLine("[yellow] Closing the Program[/]");
                Environment.Exit(0);
            }
            else if (ChosenCategory == "Favorites")
            {
                DatabaseController.ShowFavorites();
                Console.ReadKey();
                UserInterface.MainMenu();
            }
            return ChosenCategory;
        }
        internal static void ShowDrinkDetail(Drink drink)
        {
            var measuresList = drink.MakeMeasuresList();
            var ingredientsList = drink.MakeIngredientsList();
            string measure = "";

            AnsiConsole.MarkupLine($"[yellow]Drink: [green]{drink.strDrink}[/]\nInstructions:[/][green]{drink.strInstructions}[/]\n");
            for (int i = 0; i < ingredientsList.Count; i++)
            {
                if (i >= measuresList.Count) measure = "Up to you";
                else measure = measuresList.ElementAt(i);
                string ingredient = ingredientsList.ElementAt(i);
                AnsiConsole.MarkupLine($"[yellow]Ingredient: [green]{ingredient}[/], Measure:[/][green]{measure}[/]\n");
            }

            DatabaseController.ShowViews(drink.strDrink);
            InsideDrinkMenu(drink);
        }

        internal static void InsideDrinkMenu(Drink drink)
        {
            var favorites = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("")
            .EnableSearch()
            .SearchPlaceholderText("[gray]Type to search[/]")
            .AddChoices<string>("Add to Favorites", "Remove from Favorites", "View Image", "Main Menu"));

            if (favorites == "Add to Favorites")
            {
                DatabaseController.AddToFavorites(drink.strDrink);
                UserInterface.MainMenu();
            }
            else if (favorites == "View Image")
            {
                ApiHandler.ShowImage(drink.strDrinkThumb);
                UserInterface.MainMenu();
            }
            else if (favorites == "Remove from Favorites")
            {
                DatabaseController.RemoveFromFavorites(drink.strDrink);
                UserInterface.MainMenu();
            }
            else UserInterface.MainMenu();
        }


    }
}