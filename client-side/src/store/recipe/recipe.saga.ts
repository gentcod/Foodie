import { all, call, put, select, takeLatest } from "typed-redux-saga/macro";
import messenger from '../../app/api/messenger';
import { fetchRecipeRatingsFailed, fetchRecipeRatingsSuccess, fetchRecipesFailed, fetchRecipesSearchFailed, fetchRecipesSearchSuccess, fetchRecipesSuccess } from "./recipe.action";
import { RECIPES_ACTION_TYPES, RECIPES_SEARCH_ACTION_TYPES, RECIPE_RATINGS_ACTION_TYPES } from "./recipe.types";
import { selectSearchParams } from "./recipe.selector";

const { Recipes } = messenger;

export const getAxiosParams = (searchString: string = document.location.search) => {
   const params = new URLSearchParams(searchString);
   return params
}

export const fetchRecipesFromApi = async (params?: URLSearchParams) => {   
   const response = await Recipes.list(params);
   return response;
}

//Recipes Saga
export function* fetchRecipesAsync() {
   try {
      const axiosParams = getAxiosParams()
      const recipes = yield* call(fetchRecipesFromApi, axiosParams);
      yield* put(fetchRecipesSuccess(recipes));
   } catch (error) {
      yield* put(fetchRecipesFailed(error as Error));
   }
}

export function* onFetchRecipes() {
   yield* takeLatest(RECIPES_ACTION_TYPES.FETCH_RECIPE_START, fetchRecipesAsync)
}


//Recipe Ratings Saga
export const fetchRecipesRatingsFromApi = async () => {
   const response = await Recipes.listRecipeRatings();
   return response;
}

export function* fetchRecipesRatingsAsync() {
   try {
      const recipesRatings = yield* call(fetchRecipesRatingsFromApi);
      yield* put(fetchRecipeRatingsSuccess(recipesRatings));
   } catch (error) {
      yield put(fetchRecipeRatingsFailed(error as Error));
   }
}

export function* onFetchRecipesRatings() {
   yield* takeLatest(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_START, fetchRecipesRatingsAsync)
}

//Recipe Search
export function* fetchRecipesSearchAsync() {
   try {
      const searchString = yield* select(selectSearchParams)
      const axiosParams = getAxiosParams(searchString)
      const recipes = yield* call(fetchRecipesFromApi, axiosParams);
      yield* put(fetchRecipesSearchSuccess(recipes));
   } catch (error) {
      yield* put(fetchRecipesSearchFailed(error as Error));
   }
}

export function* onFetchRecipesSearch() {
   yield* takeLatest(RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_START, fetchRecipesSearchAsync)
}

//Saga
export function* recipesSaga() {
   yield* all([call(onFetchRecipes), call(onFetchRecipesRatings), call(onFetchRecipesSearch)]);
}

