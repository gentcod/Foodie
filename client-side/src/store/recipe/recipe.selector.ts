import { createSelector } from "reselect";
import { RecipeState } from "./recipe.reducer";

const selectRecipeReducer = (state: any): RecipeState => state.recipes;

export const selectRecipes = createSelector(
   [selectRecipeReducer],
   (recipesSlice) => recipesSlice.recipes
)