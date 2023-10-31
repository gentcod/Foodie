import { AnyAction } from 'redux'
import { Recipe, } from '../../app/models/recipes'
import { fetchRecipesStart, fetchRecipesSuccess, fetchRecipesFailed, fetchRecipeRatingsStart, fetchRecipeRatingsSuccess, fetchRecipesSearchStart, fetchRecipesSearchSuccess, fetchRecipesSearchFailed, } from './recipe.action';
import { Rating } from '../../app/models/ratings';

//Recipes
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


//Recipe Ratings
export type RecipeRatingsState = {
   readonly recipeRatings: Rating[];
   readonly isLoading: boolean;
   readonly error?: Error | null;
}

const RECIPES_RATINGS_INITIAL_STATE: RecipeRatingsState = {
   recipeRatings: [],
   isLoading: false,
   error: null,   
}

export const recipesRatingsReducer = (state = RECIPES_RATINGS_INITIAL_STATE, action = {} as AnyAction) => {
   if (fetchRecipeRatingsStart.match(action))
      return {
         ...state,
         isLoading: true,
      }
   if (fetchRecipeRatingsSuccess.match(action))
      return {
         ...state,
         recipeRatings: action.payload,
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


//Recipe Search
export type RecipesSearchState = {
   readonly searchString: string;
   readonly recipes: Recipe[];
   readonly isLoading: boolean;
   readonly error?: Error | null;
}

const RECIPES_SEARCH_INITIAL_STATE: RecipesSearchState = {
   searchString: "",
   recipes: [],
   isLoading: false,
   error: null,
}

export const recipesSearchReducer = (state = RECIPES_SEARCH_INITIAL_STATE, action = {} as AnyAction) => {
   if (fetchRecipesSearchStart.match(action)) {
      return {
         ...state,
         searchString: action.payload,
         isLoading: true,
      }
   }
   if (fetchRecipesSearchSuccess.match(action))
      return {
         ...state,
         recipes: action.payload,
         isLoading: false,
      }
   if (fetchRecipesSearchFailed.match(action))
      return {
         ...state,
         error: action.payload,
         isLoading: false,
      }

   return state;
}