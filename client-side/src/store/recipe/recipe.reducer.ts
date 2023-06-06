import { RECIPES_ACTION_TYPES, Recipe } from './recipe.types'
import { RecipeAction } from './recipe.action';

export type RecipeState = {
   readonly recipes: Recipe[];
   readonly error?: Error | null;
}

const RECIPES_INITIAL_STATE: RecipeState = {
   recipes: [],
   // error: null
}

export const recipesReducer = (state = RECIPES_INITIAL_STATE, action = {} as RecipeAction) => {
   switch (action.type) {
      case RECIPES_ACTION_TYPES.FETCH_RECIPE_START:
         return {
            ...state
         };
      case RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS:
         return {
            ...state,
            recipes: action.payload
         }
      case RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED:
         return {
            ...state,
            error: action.payload
         }
      default:
         return state;
   }
}