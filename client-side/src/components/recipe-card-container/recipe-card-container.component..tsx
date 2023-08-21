import { useEffect } from "react";
import { fetchRecipesStart } from "../../store/recipe/recipe.action";
import { selectRecipes } from "../../store/recipe/recipe.selector";
import { useDispatch, useSelector } from "react-redux";

import RecipeCard from "../recipe-card/recipe-card.component";

import { CardContainer } from './recipe-card-container.style';



const RecipeCardContainer = () => {
   const dispatch = useDispatch();
   useEffect(() => {
     dispatch(fetchRecipesStart());
   }, [dispatch])
 
   const data = useSelector(selectRecipes);
   console.log(data);

   return (
      <CardContainer>
         {
            data.map(el => (
               <RecipeCard key={el.id} name={el.name} origin={el.origin} cookTime={el.cookTime} description={el.description} imgSrc="images/foodie.png"/>
            ))
         }
      </CardContainer>
   )
}

export default RecipeCardContainer;