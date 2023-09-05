import { useEffect } from "react";
import { fetchRecipesStart } from "../../store/recipe/recipe.action";
import { selectRecipeIsLoading, selectRecipes } from "../../store/recipe/recipe.selector";
import { useDispatch, useSelector } from "react-redux";

import RecipeCard from "../recipe-card/recipe-card.component";

import { CardContainer } from './recipe-card-container.style';
import Loading from "../loading/loading.component";
import LoadingComp from "../loading-comp/loading-comp.component";



const RecipeCardContainer = () => {
   const dispatch = useDispatch();
   useEffect(() => {
     dispatch(fetchRecipesStart());
   }, [dispatch])
 
   const data = useSelector(selectRecipes);
   const isLoading = useSelector(selectRecipeIsLoading);

   return (
      <CardContainer>
         {
            isLoading ?
            <LoadingComp/> :
            data.map(el => (
               isLoading ? <Loading/> : <RecipeCard key={el.id} name={el.name} origin={el.origin} cookTime={el.cookTime} description={el.description} imgSrc={el.imageSrc}/>
            ))
         }
      </CardContainer>
   )
}

export default RecipeCardContainer;