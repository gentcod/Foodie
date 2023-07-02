import RecipeCategoryCard from '../recipe-category-card/recipe-category-card.component';
import { Container } from './recipe-category-container.style';

type RecipeDataProps = {
   id: number; 
   name: string;
   description: string;
   imgSrc: string;
   link: string;
}[]

const data: RecipeDataProps = [
   {
      id: 0,
      name: 'Foreign Recipes',
      description: 'A compilation of various recipes from different parts of the world',
      imgSrc: 'images/foreign.jpg',
      link: '',
   },

   {
      id: 1,
      name: 'Pastry Recipes',
      description: 'A compilation of various pastries and bakes',
      imgSrc: 'images/pastries.jpg',
      link: '',
   },

   {
      id: 2,
      name: 'Vegetarian Recipes',
      description: 'A compilation of various vegetarian diets',
      imgSrc: 'images/vegetarian.jpg',
      link: '',
   },

   {
      id: 3,
      name: 'Fast Recipes',
      description: 'A compilation of various easily made delicacies',
      imgSrc: 'images/tacos.jpg',
      link: '',
   },
   
   {
      id: 4,
      name: 'African Recipes',
      description: 'A compilation of various african Dishes',
      imgSrc: 'images/featured.jpg',
      link: '',
   },
]

const RecipeCategoryContainer = () => {
   return (
      <Container>
         {data.map(el => <RecipeCategoryCard key={el.id} name={el.name} description={el.description} imgSrc={el.imgSrc} link={el.link}/>)}
      </Container>
   )
}

export default RecipeCategoryContainer;