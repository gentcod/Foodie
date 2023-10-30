type RecipeDataProps = {
   id: number; 
   name: string;
   description: string;
   imgSrc: string;
   link: string;
}[]

export const data: RecipeDataProps = [
   {
      id: 0,
      name: 'Foreign Recipes',
      description: 'A compilation of various recipes from different parts of the world',
      imgSrc: 'https://i.ibb.co/rGK2CC5/foreign.webp',
      link: '',
   },

   {
      id: 1,
      name: 'Pastry Recipes',
      description: 'A compilation of various pastries and bakes',
      imgSrc: 'https://i.ibb.co/7nhBWx9/pastries.webp',
      link: '',
   },

   {
      id: 2,
      name: 'Vegetarian Recipes',
      description: 'A compilation of various vegetarian diets',
      imgSrc: 'https://i.ibb.co/qCTVxJF/vegetarian.webp',
      link: '',
   },

   {
      id: 3,
      name: 'Fast Recipes',
      description: 'A compilation of various easily made delicacies',
      imgSrc: 'https://i.ibb.co/GtSRLts/tacos.webp',
      link: '',
   },
   
   {
      id: 4,
      name: 'African Recipes',
      description: 'A compilation of various african Dishes',
      imgSrc: 'https://i.ibb.co/M5dhwvy/featured.webp',
      link: '',
   },
]
