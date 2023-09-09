import { useEffect } from "react";
import { fetchRecipeRatingsStart, fetchRecipesStart } from "../../store/recipe/recipe.action";
import {
  selectRecipeIsLoading,
  selectRecipes,
  selectRecipesRatings,
} from "../../store/recipe/recipe.selector";
import { useDispatch, useSelector } from "react-redux";

import RecipeCard from "../../components/recipe-card/recipe-card.component";

import { CardContainer } from "./recipe-preview.style";
import Loading from "../../components/loading/loading.component";
import LoadingComp from "../../components/loading-comp/loading-comp.component";
import { useParams } from "react-router-dom";
import { Recipe } from "../../store/recipe/recipe.types";

const RecipeCardContainer = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRecipesStart());
    dispatch(fetchRecipeRatingsStart());
  }, [dispatch]);

  const allRecipes = useSelector(selectRecipes);
  const isLoading = useSelector(selectRecipeIsLoading);
  const recipeRatings = useSelector(selectRecipesRatings);
  console.log('ratings: ',recipeRatings);

  const params = useParams();

  let data;
  data = params.recipeCat === 'recipe' ?  [] as Recipe[] : allRecipes; 

  return (
    <CardContainer>
      {isLoading ? (
        <LoadingComp />
      ) : (
        data.map((el) =>
          isLoading ? (
            <Loading />
          ) : (
            <RecipeCard
              key={el.id}
              name={el.name}
              origin={el.origin}
              cookTime={el.cookTime}
              description={el.description}
              imgSrc={el.imageSrc}
            />
          )
        )
      )}

      {/* {params.recipeCat === 'rec' ? <RecipeCard name="Trial" origin="Trial" cookTime="Trial" description="Trial" imgSrc="Trial"/> : ''} */}
    </CardContainer>
  );
};

export default RecipeCardContainer;
