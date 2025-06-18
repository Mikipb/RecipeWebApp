using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RecipeWebApp.Models;
using RecipeWebApp.Data;

namespace RecipeWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _db;

        public SearchController(HttpClient httpClient, ApplicationDbContext applicationDbContext)
        {
            _httpClient = httpClient;
            _db = applicationDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult>Index(string? userInput)
        {
            ViewData["Title"] = "Search";
            // Prvo pokušaj da userInput popuniš iz TempData ako nije došao iz query-ja
            if (string.IsNullOrEmpty(userInput) && TempData["LastSearch"] != null)
            {
                userInput = TempData["LastSearch"].ToString();
                TempData.Keep("LastSearch"); // da ostane za dalje ako treba opet
            }

            if (TempData["AlertMessage"] != null)
            {
                ViewBag.Message = TempData["AlertMessage"];
                ViewBag.AlertType = TempData["AlertType"];
            }

            if (string.IsNullOrWhiteSpace(userInput))
            {
                // Ako je prazno i dolazi iz forme
                if (Request.Query.ContainsKey("userInput"))
                {
                    TempData["AlertMessage"] = "Enter recipe for search.";
                    TempData["AlertType"] = "warning";
                    return View(new List<Meal>());
                }

                // Ako samo otvaraš /Search bez inputa, vrati prazan view bez poruke
                return View(new List<Meal>());
            }

            // Ako unos sadrži nedozvoljene karaktere
            if (!Regex.IsMatch(userInput, @"^[\p{L}\s]+$"))
            {
                TempData["AlertMessage"] = "Content can only contain letters.";
                TempData["AlertType"] = "warning";
                return View(new List<Meal>());
            }

            string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={userInput}";
            TempData["LastSearch"] = userInput;

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Deserijalizacija JSON odgovora u `Meals` objekat
                    var mealsData = JsonConvert.DeserializeObject<Meals>(json);

                    if ((mealsData?.meals == null || mealsData.meals.Count == 0) && Request.Query.ContainsKey("userInput"))
                    {
                        TempData["AlertMessage"] = "No meals found for your search.";
                        TempData["AlertType"] = "warning";
                        return View(new List<Meal>());
                    }

                    return View(mealsData.meals); // Prosleđujemo listu u View
                }
                else
                {
                    ViewBag.Message = $"API error: {response.StatusCode}";
                    return View(new List<Meal>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Exception during API call: {ex.Message}";
                return View(new List<Meal>());
            }
        }

        public async Task<IActionResult>MealInfo(string idMeal)
        {
            if (string.IsNullOrEmpty(idMeal))
            {
                ViewBag.Message = "Invalid or missing meal ID.";
                return View("Error");
            }

            if (TempData["LastSearch"] != null)
            {
                ViewBag.LastSearch = TempData["LastSearch"].ToString();
                TempData.Keep("LastSearch"); // da ostane dostupno za sledeći put ako treba
            }

            bool alreadyInFavorites = _db.Meals.Any(f => f.idMeal == idMeal);
            if (alreadyInFavorites)
            {
                var mealInDb = _db.Meals.FirstOrDefault(f => f.idMeal == idMeal);
                return View("AlreadyAdded",mealInDb); 
            }

            string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={idMeal}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = $"API call failed with status: {response.StatusCode}";
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();

            var listOfMeals = JsonConvert.DeserializeObject<Meals>(json);

            if (listOfMeals == null || listOfMeals.meals == null || listOfMeals.meals.Count == 0)
            {
                ViewBag.Message = "Meal not found.";
                return View("Error");
            }

            var meal = listOfMeals.meals[0];

            if (meal == null)
            {
                ViewBag.Message = "Meal is null.";
                return View("Error");
            }

            return View(meal);
        }
    }
}
