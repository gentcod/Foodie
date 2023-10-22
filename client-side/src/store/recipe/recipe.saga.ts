import { all, call, put, select, takeLatest } from "typed-redux-saga/macro";
import messenger from '../../app/api/messenger';
import { fetchRecipeRatingsFailed, fetchRecipeRatingsSuccess, fetchRecipesFailed, fetchRecipesSuccess } from "./recipe.action";
import { RECIPES_ACTION_TYPES, RECIPE_RATINGS_ACTION_TYPES } from "./recipe.types";
import { RecipeParams } from "../../app/models/recipes";
import { useParams } from "react-router-dom";

const { Recipes } = messenger;

export const GetLocation = () => {
   const params = useParams()
   console.log(params)

   const axiosParams: RecipeParams = {
      sortBy: params.sortBy!,
      search: params.search,
      orderBy: +params.orderBy!,
   }
   
   return axiosParams;
}

export const getAxiosParams = (recipeParams: RecipeParams) => {
   const params = new URLSearchParams();
   console.log(recipeParams)

   if (recipeParams.search) params.append('search', recipeParams.search);
   console.log(params)
   return params
}

export const fetchRecipesFromApi = async () => {   
   const response = await Recipes.list();
   return response;
}

export const fetchRecipeSRatingsFromApi = async () => {
   const response = await Recipes.listRecipeRatings();
   return response;
}

export const fetchRecipesSearchFromApi = async (params: URLSearchParams) => {   
   const response = await Recipes.list(params);
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

export function* fetchRecipesSearchAsync() {
   try {
      const params = GetLocation()
      const axiosParams = getAxiosParams(params)
      const recipes = yield* call(fetchRecipesSearchFromApi, axiosParams);
      yield* put(fetchRecipesSuccess(recipes));
   } catch (error) {
      yield* put(fetchRecipesFailed(error as Error));
   }
}

export function* onFetchRecipes() {
   yield* takeLatest(RECIPES_ACTION_TYPES.FETCH_RECIPE_START, fetchRecipesAsync)
}

export function* onFetchRecipesRatings() {
   yield* takeLatest(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_START, fetchRecipesRatingsAsync)
}

export function* onFetchRecipesSearch() {
   yield* takeLatest(RECIPES_ACTION_TYPES.FETCH_RECIPE_START, fetchRecipesSearchAsync)
}

export function* recipesSaga() {
   yield* all([call(onFetchRecipes), call(onFetchRecipesRatings), call(onFetchRecipesSearch)]);
}
