import { all, call, put, takeLatest } from "typed-redux-saga/macro";
// import { takeLatest, all, call, put } from 'typed-redux-saga/macro';
import messenger from '../../app/messenger';
import { fetchRecipesFailed, fetchRecipesSuccess } from "./recipe.action";
import { RECIPES_ACTION_TYPES} from "./recipe.types";

const { Recipes } = messenger;

// export const fetchRecipesFromApi = async (): Promise<Recipe[]> => {
//    const response = await Recipes.list();
//    return response;
// }
export const fetchRecipesFromApi = async () => {
   const response = await Recipes.list();
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

export function* onFetchRecipes() {
   yield* takeLatest(RECIPES_ACTION_TYPES.FETCH_RECIPE_START, fetchRecipesAsync)
}

export function* recipesSaga() {
   yield* all([call(onFetchRecipes)]);
}
