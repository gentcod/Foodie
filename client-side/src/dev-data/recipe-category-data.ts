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
      imgSrc: 'https://i.ibb.co/CQHW8Bn/foreign.jpg',
      link: '',
   },

   {
      id: 1,
      name: 'Pastry Recipes',
      description: 'A compilation of various pastries and bakes',
      imgSrc: 'https://i.ibb.co/rt7gm6L/pastries.jpg',
      link: '',
   },

   {
      id: 2,
      name: 'Vegetarian Recipes',
      description: 'A compilation of various vegetarian diets',
      imgSrc: 'https://i.ibb.co/HzG8Gf9/vegetarian.jpg',
      link: '',
   },

   {
      id: 3,
      name: 'Fast Recipes',
      description: 'A compilation of various easily made delicacies',
      imgSrc: 'https://i.ibb.co/TqnwP5p/tacos.jpg',
      link: '',
   },
   
   {
      id: 4,
      name: 'African Recipes',
      description: 'A compilation of various african Dishes',
      imgSrc: 'https://i.ibb.co/MDf4CtV/featured.jpg',
      link: '',
   },
]
