# 🍽️ Recipe Web App

A simple ASP.NET Core MVC web application for searching, viewing, and saving meal recipes using a REST API and SQL database.

## 📌 Project Overview

Recipe Web App allows users to:

- Search for meals using an external API ([TheMealDB](https://www.themealdb.com/))
- View detailed information about meals (name, image, ingredients, instructions)
- Add meals to a list of favorites
- View and delete favorite meals
- Store favorites locally using MS SQL and Entity Framework

## 🛠️ Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- MS SQL Server (LocalDB)
- [TheMealDB API](https://www.themealdb.com/api.php)
- Newtonsoft.Json
- Bootstrap 5

## 📸 Screenshots

| Search Meals | Meal Details | Favorite Meals |
|--------------|--------------|----------------|
| ![search](screenshots/search.png) | ![details](screenshots/details.png) | ![favorites](screenshots/favorites.png) |

> *(You can add screenshots in a `wwwroot/images/screenshots/` folder and adjust the paths above.)*

## ⚙️ Getting Started

### 1. Clone the repository:

```bash
git clone https://github.com/Mikipb/RecipeWebApp.git
cd RecipeWebApp
