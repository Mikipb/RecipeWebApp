using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatingMealTableInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    idMeal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    strMeal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strMealThumb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strYoutube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strSource = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.idMeal);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
