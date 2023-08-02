using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecipeRatingsDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingRecipe_Recipes_RecipeId",
                table: "RatingRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingRecipe",
                table: "RatingRecipe");

            migrationBuilder.RenameTable(
                name: "RatingRecipe",
                newName: "RecipeRatings");

            migrationBuilder.RenameIndex(
                name: "IX_RatingRecipe_RecipeId",
                table: "RecipeRatings",
                newName: "IX_RecipeRatings_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeRatings",
                table: "RecipeRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeRatings",
                table: "RecipeRatings");

            migrationBuilder.RenameTable(
                name: "RecipeRatings",
                newName: "RatingRecipe");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeRatings_RecipeId",
                table: "RatingRecipe",
                newName: "IX_RatingRecipe_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingRecipe",
                table: "RatingRecipe",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingRecipe_Recipes_RecipeId",
                table: "RatingRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
