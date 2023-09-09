import { all, call, put, takeLatest } from "typed-redux-saga/macro";
import messenger from '../../app/messenger';
import { fetchRecipeRatingsFailed, fetchRecipeRatingsSuccess, fetchRecipesFailed, fetchRecipesSuccess } from "./recipe.action";
import { RECIPES_ACTION_TYPES, RECIPE_RATINGS_ACTION_TYPES} from "./recipe.types";

const { Recipes } = messenger;

export const fetchRecipesFromApi = async () => {
   const response = await Recipes.list();
   return response;
}

export const fetchRecipeSRatingsFromApi = async () => {
   const response = await Recipes.listRecipeRatings();
   return response;
}

export function* fetchRecipesAsync() {
   try {
      const recipes = yield* call(fetchRecipesFromApi);
      yield* put(fetchRecipesSuccess(recipes));
   } catch (error) {
      yield* put(fetchRecipesFailed(error as Error));
   }
}

export function* fetchRecipesRatingsAsync() {
   try {
      const recipesRatings = yield* call(fetchRecipeSRatingsFromApi);
      yield* put(fetchRecipeRatingsSuccess(recipesRatings));
   } catch (error) {
      yield put(fetchRecipeRatingsFailed(error as Error));
   }
}

export function* onFetchRecipes() {
   yield* takeLatest(RECIPES_ACTION_TYPES.FETCH_RECIPE_START, fetchRecipesAsync)
}

export function* onFetchRecipesRatings() {
   yield* takeLatest(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_START, fetchRecipesRatingsAsync)
}

export function* recipesSaga() {
   yield* all([call(onFetchRecipes), call(onFetchRecipesRatings)]);
}
