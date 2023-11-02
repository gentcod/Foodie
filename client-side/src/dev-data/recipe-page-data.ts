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
      heading: "Top Recipe Categories",
      contents: [
         {
            id: 0,
            name: "Grills",
            imgSrc: "https://i.ibb.co/zrrqBY7/grills-cat.webp",
         },

         {
            id: 1,
            name: "Vegetables",
            imgSrc: "https://i.ibb.co/Bnkyk6Z/vegetables-cat.webp",
         },

         {
            id: 2,
            name: "Rice",
            imgSrc: "https://i.ibb.co/r6J8D2p/rice-cat.webp",
         },
      ],
   },
];