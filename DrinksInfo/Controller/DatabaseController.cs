using Dapper;
using DrinksInfo.View;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Configuration;

namespace DrinksInfo.Controller
{
    internal class DatabaseController
    {
        internal static SqliteConnection OpenConnection()
        {
            string? ConnectionString = ConfigurationManager.AppSettings["ConnectionStringKey"];
            SqliteConnection connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
        internal static void CreateTable()
        {
            var connection = OpenConnection();
            var sql = @"CREATE TABLE IF NOT EXISTS FavoriteDrinks(
                                                   FavoriteDrinkId INTEGER PRIMARY KEY AUTOINCREMENT,
                                                   DrinkName TEXT UNIQUE);
                        CREATE TABLE IF NOT EXISTS DrinkViews(
                                                   DrinkViewsId INTEGER PRIMARY KEY AUTOINCREMENT,
                                                   DrinkName TEXT UNIQUE,
                                                   ViewsCount INTEGER DEFAULT '0');";
            connection.Execute(sql);
            connection.Close();
        }

        internal static void ShowFavorites()
        {
            var connection = OpenConnection();
            var sql = "SELECT DrinkName FROM FavoriteDrinks";
            List<string> Favorites = new();
            HttpClient client = new();
            ApiHandler handler = new();

            try
            {
                Favorites = connection.Query<string>(sql).ToList();
                Favorites.Add("Cancel");

            }
            catch (SqliteException)
            {
                AnsiConsole.MarkupLine("\n [red] Something went wrong, Returning to Main Menu [/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }

            if (Favorites.Count <= 1)
            {
                AnsiConsole.MarkupLine("[red] No Favorites right now, returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Favorites menu")
                .AddChoices<string>(Favorites));

            if (choice == "Cancel") UserInterface.MainMenu();
            var DrinkInfo = handler.GetDrinkDetails(client, choice);
            UserInterface.ShowDrinkDetail(DrinkInfo.Result);
        }

        internal static void AddToFavorites(string strDrinkName)
        {
            var connection = OpenConnection();
            var parameters = new { name = strDrinkName.Trim() };
            var sql = @"INSERT INTO FavoriteDrinks(DrinkName) VALUES (@name)";
            try
            {
                connection.Execute(sql, parameters);
            }
            catch (SqliteException)
            {
                AnsiConsole.MarkupLine("[red]Drink is already in Favorites, Returning to Main Menu[/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
            AnsiConsole.MarkupLine($"[blue]{strDrinkName}[/] [yellow]added to Favorites[/]");
            Console.ReadKey();
            UserInterface.MainMenu();
            connection.Close();
        }

        internal static void RemoveFromFavorites(string strDrinkName)
        {
            var connection = OpenConnection();
            var parameters = new { name = strDrinkName.Trim() };
            var sql = "DELETE FROM FavoriteDrinks WHERE DrinkName = @name";

            try
            {
                var RowsAffected = connection.Execute(sql, parameters);
                if (RowsAffected == 0)
                {
                    AnsiConsole.MarkupLine($"\n [red] {strDrinkName} is Not in Favorites, Returning to Main Menu[/]");
                    Console.ReadKey();
                    UserInterface.MainMenu();
                }
            }
            catch (SqliteException)
            {
                AnsiConsole.MarkupLine("\n [red] Something went wrong, Returning to Main Menu [/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
            AnsiConsole.MarkupLine($"\n [blue]{strDrinkName}[/] [yellow]removed from Favorites[/]");
            Console.ReadKey();
            connection.Close();
        }
        internal static void IncrementViews(string strDrinkName)
        {
            var connection = OpenConnection();
            var parameters = new { name = strDrinkName.Trim() };
            var InsertSql = @"INSERT OR IGNORE INTO DrinkViews (DrinkName, ViewsCount) VALUES (@name, 0);";

            try
            {
                connection.Execute(InsertSql, parameters);
            }
            catch (SqliteException)
            {
                AnsiConsole.MarkupLine("\n [red] Something went wrong, Returning to Main Menu [/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
            var IncrementSql = @"UPDATE DrinkViews SET ViewsCount = ViewsCount + 1 WHERE DrinkName = @name";
            connection.Execute(IncrementSql, parameters);
            connection.Close();
        }

        internal static void ShowViews(string strDrinkName)
        {
            var connection = OpenConnection();
            var parameters = new { name = strDrinkName.Trim() };
            var sql = "SELECT ViewsCount FROM DrinkViews WHERE DrinkName = @name";
            try
            {
                var views = connection.QuerySingleOrDefault<int>(sql, parameters).ToString();
                AnsiConsole.MarkupLine($"[yellow]View Count:[/][blue] {views}[/]");
            }
            catch (SqliteException)
            {
                AnsiConsole.MarkupLine("\n [red] Something went wrong, Returning to Main Menu [/]");
                Console.ReadKey();
                UserInterface.MainMenu();
            }
        }
    }
}
