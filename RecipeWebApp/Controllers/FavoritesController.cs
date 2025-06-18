using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeWebApp.Data;
using RecipeWebApp.Models;

namespace RecipeWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;

        public FavoritesController(ApplicationDbContext db, HttpClient httpClient)
        {
            _db = db;
            _httpClient = httpClient;
        }


        [HttpPost]
        public async Task<IActionResult>AddToFavorites(string idMeal)
        {
            string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={idMeal}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var listOfMeals = JsonConvert.DeserializeObject<Meals>(json);
            var meal = listOfMeals.meals[0];

            if (TempData["LastSearch"] != null)
            {
                ViewBag.LastSearch = TempData["LastSearch"]?.ToString();
                TempData.Keep("LastSearch"); // da ostane dostupno za sledeći put ako treba
            }

            TempData["AlertMessage"] = $"You added '{meal.strMeal}' to favorites.";
            TempData["AlertType"] = "success"; // za zeleni alert

            //dodavanje jela u tabelu u bazu podataka
            _db.Meals.Add(meal);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index","Search");
        }

        [HttpPost]
        public async Task<IActionResult>RemoveFromFavorites(string idMeal)
        {
            // Pamti poslednju pretragu da možeš vratiti korisnika na istu pretragu
            if (TempData["LastSearch"] != null)
            {
                ViewBag.LastSearch = TempData["LastSearch"]?.ToString();
                TempData.Keep("LastSearch"); // da ostane dostupno za sledeći put ako treba
            }

            var mealInDb = await _db.Meals.FindAsync(idMeal);
            if (mealInDb != null)
            {
                _db.Meals.Remove(mealInDb);
                await _db.SaveChangesAsync();
            }

            TempData["AlertMessage"] = $"You removed '{mealInDb.strMeal}' from favorites.";
            TempData["AlertType"] = "danger"; // za crveni alert
            return RedirectToAction("Index", "Search");
        }

        public async Task<IActionResult> RemoveFromFavoritesTab(string idMeal)
        {
            var mealInDb = await _db.Meals.FindAsync(idMeal);
            if (mealInDb != null)
            {
                _db.Meals.Remove(mealInDb);
                await _db.SaveChangesAsync();
            }

            TempData["AlertMessage"] = $"You removed '{mealInDb?.strMeal}' from favorites.";
            TempData["AlertType"] = "danger"; // za crveni alert
            return RedirectToAction("Index", "Favorites");
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Favorites";
            var allMeals = await _db.Meals.ToListAsync();
            return View(allMeals);
        }
    }
}
