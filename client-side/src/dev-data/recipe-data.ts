type RecipeItemProp = {
   id: number;
   name: string;
   imgSrc: string;
};

type RecipeCategoryProp = {
   id: number;
   heading: string;
   contents: RecipeItemProp[];
}[];

export const categoryData: RecipeCategoryProp = [
   {
      id: 0,
      heading: "Top Rated Recipes",
      contents: [
         {
            id: 0,
            name: "Grills",
            imgSrc: "",
         },

         {
            id: 1,
            name: "Vegetables",
            imgSrc: "",
         },

         {
            id: 2,
            name: "Rice",
            imgSrc: "",
         },
      ],
   },
];