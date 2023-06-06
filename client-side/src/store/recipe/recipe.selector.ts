import { createSelector } from "reselect";

import { RecipeState } from "./recipe.reducer";
const selectRecipeReducer = (state: RecipeState) => state.recipes

export const selectRecipes = createSelector(
   [selectRecipeReducer],
   (recipesSlice) => recipesSlice
)