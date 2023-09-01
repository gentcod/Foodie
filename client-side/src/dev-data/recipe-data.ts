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
            imgSrc: "https://i.ibb.co/4fN1Fzf/grills-cat.jpg",
         },

         {
            id: 1,
            name: "Vegetables",
            imgSrc: "https://i.ibb.co/MDp3B2q/vegetables-cat.jpg",
         },

         {
            id: 2,
            name: "Rice",
            imgSrc: "https://i.ibb.co/vX0r6p1/rice-cat.jpg",
         },
      ],
   },
];