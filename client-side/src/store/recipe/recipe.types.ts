export enum RECIPES_ACTION_TYPES {
   FETCH_RECIPE_START = 'recipe/FETCH_RECIPE_START',
   FETCH_RECIPE_SUCCESS = 'recipe/FETCH_RECIPE_SUCCESS',
   FETCH_RECIPE_FAILED = 'recipe/FETCH_RECIPE_FAILED',
};

export enum RECIPE_RATINGS_ACTION_TYPES {
   FETCH_RECIPE_RATINGS_START = 'recipe/FETCH_RECIPE_RATINGS_START',
   FETCH_RECIPE_RATINGS_SUCCESS = 'recipe/FETCH_RECIPE_RATINGS_SUCCESS',
   FETCH_RECIPE_RATINGS_FAILED = 'recipe/FETCH_RECIPE_FAILED',
}

export type Recipe = {
   id: number,
   name: string,
   ingredients: string,
   description: string,
   origin: string,
   cookTime: string;
   imageSrc: string;
}