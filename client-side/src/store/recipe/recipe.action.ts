import { createAction, ActionWithPayload, Action } from "../../utils/reducer/reducer.utilities";
import { RECIPES_ACTION_TYPES, Recipe } from "./recipe.types";

export type SetRecipes = ActionWithPayload<RECIPES_ACTION_TYPES.SET_RECIPES, Recipe[]>;
export type FetchRecipesStart = Action<RECIPES_ACTION_TYPES.FETCH_RECIPE_START>;
export type FetchRecipesSuccess = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, Recipe[]>;
export type FetchRecipesFailed = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, Error>;

export type RecipeAction = SetRecipes | FetchRecipesStart | FetchRecipesSuccess | FetchRecipesFailed;

export const setRecipes = (recipes: Recipe[]) : SetRecipes => createAction(RECIPES_ACTION_TYPES.SET_RECIPES, recipes);
export const fetchRecipesStart = () : FetchRecipesStart => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_START);
export const fetchRecipesSuccess = (recipes: Recipe[]) : FetchRecipesSuccess => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, recipes);
export const fetchRecipesFailed = (error: Error) : FetchRecipesFailed => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, error);
