namespace DrinksInfo.Model
{
    public record class Categories()
    {
        public string strCategory { set; get; }
    }

    public record class Drink()
    {
        public string? strDrink { get; set; }
        public string? idDrink { get; set; }
        public string? strInstructions { get; set; }
        public string? strDrinkThumb { get; set; }
        public string? strIngredient1 { get; set; }
        public string? strMeasure1 { get; set; }
        public string? strIngredient2 { get; set; }
        public string? strMeasure2 { get; set; }
        public string? strIngredient3 { get; set; }
        public string? strMeasure3 { get; set; }
        public string? strIngredient4 { get; set; }
        public string? strMeasure4 { get; set; }
        public string? strIngredient5 { get; set; }
        public string? strMeasure5 { get; set; }
        public string? strIngredient6 { get; set; }
        public string? strMeasure6 { get; set; }
        public string? strIngredient7 { get; set; }
        public string? strMeasure7 { get; set; }
        public string? strIngredient8 { get; set; }
        public string? strMeasure8 { get; set; }
        public string? strIngredient9 { get; set; }
        public string? strMeasure9 { get; set; }
        public string? strIngredient10 { get; set; }
        public string? strMeasure10 { get; set; }
        public string? strIngredient11 { get; set; }
        public string? strMeasure11 { get; set; }
        public string? strIngredient12 { get; set; }
        public string? strMeasure12 { get; set; }
        public string? strIngredient13 { get; set; }
        public string? strMeasure13 { get; set; }
        public string? strIngredient14 { get; set; }
        public string? strMeasure14 { get; set; }
        public string? strIngredient15 { get; set; }
        public string? strMeasure15 { get; set; }

        public List<string> MakeMeasuresList()
        {
            var measuresList = new List<string>();

            // Add ingredients to the list if they are not null
            if (!string.IsNullOrEmpty(strMeasure1)) measuresList.Add(strMeasure1);
            if (!string.IsNullOrEmpty(strMeasure2)) measuresList.Add(strMeasure2);
            if (!string.IsNullOrEmpty(strMeasure3)) measuresList.Add(strMeasure3);
            if (!string.IsNullOrEmpty(strMeasure4)) measuresList.Add(strMeasure4);
            if (!string.IsNullOrEmpty(strMeasure5)) measuresList.Add(strMeasure5);
            if (!string.IsNullOrEmpty(strMeasure6)) measuresList.Add(strMeasure6);
            if (!string.IsNullOrEmpty(strMeasure7)) measuresList.Add(strMeasure7);
            if (!string.IsNullOrEmpty(strMeasure8)) measuresList.Add(strMeasure8);
            if (!string.IsNullOrEmpty(strMeasure9)) measuresList.Add(strMeasure9);
            if (!string.IsNullOrEmpty(strMeasure10)) measuresList.Add(strMeasure10);
            if (!string.IsNullOrEmpty(strMeasure11)) measuresList.Add(strMeasure11);
            if (!string.IsNullOrEmpty(strMeasure12)) measuresList.Add(strMeasure12);
            if (!string.IsNullOrEmpty(strMeasure13)) measuresList.Add(strMeasure13);
            if (!string.IsNullOrEmpty(strMeasure14)) measuresList.Add(strMeasure14);
            if (!string.IsNullOrEmpty(strMeasure15)) measuresList.Add(strMeasure15);

            return measuresList;
        }
        public List<string> MakeIngredientsList()
        {
            var ingredientsList = new List<string>();

            // Add ingredients to the list if they are not null
            if (!string.IsNullOrEmpty(strIngredient1)) ingredientsList.Add(strIngredient1);
            if (!string.IsNullOrEmpty(strIngredient2)) ingredientsList.Add(strIngredient2);
            if (!string.IsNullOrEmpty(strIngredient3)) ingredientsList.Add(strIngredient3);
            if (!string.IsNullOrEmpty(strIngredient4)) ingredientsList.Add(strIngredient4);
            if (!string.IsNullOrEmpty(strIngredient5)) ingredientsList.Add(strIngredient5);
            if (!string.IsNullOrEmpty(strIngredient6)) ingredientsList.Add(strIngredient6);
            if (!string.IsNullOrEmpty(strIngredient7)) ingredientsList.Add(strIngredient7);
            if (!string.IsNullOrEmpty(strIngredient8)) ingredientsList.Add(strIngredient8);
            if (!string.IsNullOrEmpty(strIngredient9)) ingredientsList.Add(strIngredient9);
            if (!string.IsNullOrEmpty(strIngredient10)) ingredientsList.Add(strIngredient10);
            if (!string.IsNullOrEmpty(strIngredient11)) ingredientsList.Add(strIngredient11);
            if (!string.IsNullOrEmpty(strIngredient12)) ingredientsList.Add(strIngredient12);
            if (!string.IsNullOrEmpty(strIngredient13)) ingredientsList.Add(strIngredient13);
            if (!string.IsNullOrEmpty(strIngredient14)) ingredientsList.Add(strIngredient14);
            if (!string.IsNullOrEmpty(strIngredient15)) ingredientsList.Add(strIngredient15);

            return ingredientsList;
        }
    }

    public record class DrinksResponse(List<Drink>? drinks);
    public record class DrinksCategories(List<Categories>? drinks);
}
