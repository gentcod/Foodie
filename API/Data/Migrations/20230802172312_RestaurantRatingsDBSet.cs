using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantRatingsDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingRestaurant_Restaurants_RestaurantId",
                table: "RatingRestaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingRestaurant",
                table: "RatingRestaurant");

            migrationBuilder.RenameTable(
                name: "RatingRestaurant",
                newName: "RestaurantRatings");

            migrationBuilder.RenameIndex(
                name: "IX_RatingRestaurant_RestaurantId",
                table: "RestaurantRatings",
                newName: "IX_RestaurantRatings_RestaurantId");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeRatings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "RestaurantRatings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantRatings",
                table: "RestaurantRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantRatings_Restaurants_RestaurantId",
                table: "RestaurantRatings",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantRatings_Restaurants_RestaurantId",
                table: "RestaurantRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantRatings",
                table: "RestaurantRatings");

            migrationBuilder.RenameTable(
                name: "RestaurantRatings",
                newName: "RatingRestaurant");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantRatings_RestaurantId",
                table: "RatingRestaurant",
                newName: "IX_RatingRestaurant_RestaurantId");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeRatings",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "RatingRestaurant",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingRestaurant",
                table: "RatingRestaurant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingRestaurant_Restaurants_RestaurantId",
                table: "RatingRestaurant",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeRatings_Recipes_RecipeId",
                table: "RecipeRatings",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
