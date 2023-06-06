export enum RECIPES_ACTION_TYPES {
   SET_RECIPES = 'recipe/SET_RECIPES',
   FETCH_RECIPE_START = 'recipe/FETCH_RECIPE_START',
   FETCH_RECIPE_SUCCESS = 'recipe/FETCH_RECIPE_SUCCESS',
   FETCH_RECIPE_FAILED = 'recipe/FETCH_RECIPE_FAILED',
};

export type Recipe = {
   id: number,
   name: string,
   ingredients: string,
   description: string,
   origin: string
}