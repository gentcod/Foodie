import { useEffect } from "react";
import {
  fetchRecipesStart,
} from "../../store/recipe/recipe.action";
import {
  selectRecipeIsLoading,
  selectRecipes,
} from "../../store/recipe/recipe.selector";
import { useDispatch, useSelector } from "react-redux";

import RecipeCard from "../recipe-card/recipe-card.component";

import { CardContainer } from "./recipe-preview.style";
import Loading from "../loading/loading.component";
import LoadingComp from "../loading-comp/loading-comp.component";
import { useLocation } from "react-router-dom";
// import { useParams } from "react-router-dom";
// import { Recipe } from "../../app/models/recipes";

const RecipeCardContainer = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRecipesStart());
  }, [dispatch]);

  const allRecipes = useSelector(selectRecipes);
  const isLoading = useSelector(selectRecipeIsLoading);

  const location = useLocation()
  console.log(location)


  let data;
  // data = params.recipeCat === "recipe" ? ([] as Recipe[]) : allRecipes; //Implement the search return here
  data = allRecipes; //Implement the search return here

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
