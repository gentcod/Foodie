import { createSelector } from "reselect";
import { RecipeRatingsState, RecipeState,} from "./recipe.reducer";

const selectRecipesReducer = (state: any): RecipeState => state.recipes;
// const selectRecipesSearchReducer = (state: any): RecipesSearchState => state.recipes;
const selectRecipesRatingsReducer = (state: any): RecipeRatingsState => state.recipesRatings


export const selectRecipes = createSelector(
   [selectRecipesReducer],
   (recipesSlice) => recipesSlice.recipes
);

export const selectRecipeIsLoading = createSelector(
   [selectRecipesReducer],
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
      
// export const selectRecipesSearch = createSelector(
//    [selectRecipesSearchReducer],
//    (recipesSearchSlice) => recipesSearchSlice.recipesSearch
// );