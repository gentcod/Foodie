import { useEffect } from "react";
import { fetchRecipesStart } from "../../store/recipe/recipe.action";
import {
  selectRecipeIsLoading,
  selectRecipes,
  // selectRecipesSearch,
} from "../../store/recipe/recipe.selector";
import { useDispatch, useSelector } from "react-redux";

import RecipeCard from "../recipe-card/recipe-card.component";

import { CardContainer } from "./recipe-preview.style";
import Loading from "../page-loading/page-loading.component";
import LoadingComp from "../loading-comp/loading-comp.component";

const RecipeCardContainer = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRecipesStart());
  }, [dispatch]);

  const allRecipes = useSelector(selectRecipes);
  const isLoading = useSelector(selectRecipeIsLoading);

  let data;
  // data = location.recipeCat === "recipe" ? ([] as Recipe[]) : allRecipes; //Implement the search return here
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
