import { createAction, ActionWithPayload, Action , withMatcher} from "../../utils/reducer/reducer.utilities";
import { RECIPES_ACTION_TYPES, RECIPE_RATINGS_ACTION_TYPES, RECIPES_SEARCH_ACTION_TYPES } from "./recipe.types";
import { Recipe, } from '../../app/models/recipes';
import { Rating } from '../../app/models/ratings';

export type FetchRecipesStart = Action<RECIPES_ACTION_TYPES.FETCH_RECIPE_START>;
export type FetchRecipesSuccess = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, Recipe[]>;
export type FetchRecipesFailed = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, Error>;

export type FetchRecipesSearchStart = ActionWithPayload<RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_START, string>;
export type FetchRecipesSearchSuccess = ActionWithPayload<RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_SUCCESS, Recipe[]>;
export type FetchRecipesSearchFailed = ActionWithPayload<RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_FAILED, Error>;

export type FetchRecipeRatingsStart = Action<RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_START>;
export type FetchRecipeRatingsSuccess = ActionWithPayload<RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_SUCCESS, Rating[]>;
export type FetchRecipeRatingsFailed = ActionWithPayload<RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_FAILED, Error>;

export const fetchRecipesStart = withMatcher(() : FetchRecipesStart => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_START));
export const fetchRecipesSuccess = withMatcher((recipes: Recipe[]) : FetchRecipesSuccess => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, recipes));
export const fetchRecipesFailed = withMatcher((error: Error) : FetchRecipesFailed => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, error));

export const fetchRecipesSearchStart = withMatcher((searchString: string) : FetchRecipesSearchStart => createAction(RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_START, searchString));
export const fetchRecipesSearchSuccess = withMatcher((recipes: Recipe[]) : FetchRecipesSearchSuccess => createAction(RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_SUCCESS, recipes));
export const fetchRecipesSearchFailed = withMatcher((error: Error) : FetchRecipesSearchFailed => createAction(RECIPES_SEARCH_ACTION_TYPES.FETCH_RECIPE_SEARCH_FAILED, error));

export const fetchRecipeRatingsStart = withMatcher(() : FetchRecipeRatingsStart => createAction(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_START));
export const fetchRecipeRatingsSuccess = withMatcher((recipeRatings: Rating[]) : FetchRecipeRatingsSuccess => createAction(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_SUCCESS, recipeRatings));
export const fetchRecipeRatingsFailed = withMatcher((error: Error) : FetchRecipeRatingsFailed => createAction(RECIPE_RATINGS_ACTION_TYPES.FETCH_RECIPE_RATINGS_FAILED, error));
