import { createAction, ActionWithPayload, Action , withMatcher} from "../../utils/reducer/reducer.utilities";
import { RECIPES_ACTION_TYPES, Recipe } from "./recipe.types";

export type FetchRecipesStart = Action<RECIPES_ACTION_TYPES.FETCH_RECIPE_START>;
export type FetchRecipesSuccess = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, Recipe[]>;
export type FetchRecipesFailed = ActionWithPayload<RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, Error>;

export const fetchRecipesStart = withMatcher(() : FetchRecipesStart => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_START));
export const fetchRecipesSuccess = withMatcher((recipes: Recipe[]) : FetchRecipesSuccess => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_SUCCESS, recipes));
export const fetchRecipesFailed = withMatcher((error: Error) : FetchRecipesFailed => createAction(RECIPES_ACTION_TYPES.FETCH_RECIPE_FAILED, error));
