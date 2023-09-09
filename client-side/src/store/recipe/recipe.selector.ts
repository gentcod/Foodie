import { createSelector } from "reselect";
import { RecipeRatingsState, RecipeState } from "./recipe.reducer";

const selectRecipeReducer = (state: any): RecipeState => state.recipes;
const selectRecipesRatingsReducer = (state: any): RecipeRatingsState => state.recipesRatings

export const selectRecipes = createSelector(
   [selectRecipeReducer],
   (recipesSlice) => recipesSlice.recipes
);

export const selectRecipeIsLoading = createSelector(
   [selectRecipeReducer],
   (isLoadingSlice) => isLoadingSlice.isLoading
);

export const selectRecipesRatings = createSelector(
   [selectRecipesRatingsReducer],
   (recipeRatingsSlice) => recipeRatingsSlice.recipeRatings
);

export const selectRecipesRatingsIsLoading = createSelector(
   [selectRecipesRatingsReducer],
   (recipeRatingsSlice) => recipeRatingsSlice.isLoading
);

//export const selectRecipesCat = createSelector(
   //    [selectRecipeReducer],
   //    (recipesSlice) => recipesSlice.recipes
   // );