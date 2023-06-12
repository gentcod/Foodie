import { AnyAction } from 'redux'
import { Recipe } from './recipe.types'
import { fetchRecipesStart, fetchRecipesSuccess, fetchRecipesFailed } from './recipe.action';

export type RecipeState = {
   readonly recipes: Recipe[];
   readonly isLoading: boolean;
   readonly error?: Error | null;
}

const RECIPES_INITIAL_STATE: RecipeState = {
   recipes: [],
   isLoading: false,
   error: null,
}

export const recipesReducer = (state = RECIPES_INITIAL_STATE, action = {} as AnyAction) => {
   if (fetchRecipesStart.match(action)) {
      return {
         ...state,
         isLoading: true,
      }
   }
   if (fetchRecipesSuccess.match(action))
      return {
         ...state,
         recipes: action.payload,
         isLoading: false,
      }
   if (fetchRecipesFailed.match(action))
      return {
         ...state,
         error: action.payload,
         isLoading: false,
      }
   return state;
}