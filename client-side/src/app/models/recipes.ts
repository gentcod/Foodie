export type Recipe = {
   id: number,
   name: string,
   ingredients: string,
   description: string,
   origin: string,
   cookTime: string;
   imageSrc: string;
}

export type RecipeParams = {
   search?: string,
   sortBy: string,
   orderBy: number,
}