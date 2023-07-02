import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchRecipesStart } from "../../store/recipe/recipe.action";
import { selectRecipes } from "../../store/recipe/recipe.selector";

import Map from "../../components/map/map.component";

import { FeaturedContainer, FeaturedImage, FeaturedTitle, HomeContainer } from "./home.style";
import RecipeCategoryContainer from "../../components/recipe-category-container/recipe-category-container.component";


const Home = () => {
   
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRecipesStart());
  }, [dispatch])

  const data = useSelector(selectRecipes);
  console.log(data);
   return (
      <HomeContainer>
        <FeaturedContainer>
          <FeaturedTitle>Featured recipe</FeaturedTitle>
          <FeaturedImage src="https://i.ibb.co/mhxQWT4/board-veg.jpg"/>
        </FeaturedContainer>
        <RecipeCategoryContainer/>
        <Map/>
      </HomeContainer>
   )
}

export default Home;