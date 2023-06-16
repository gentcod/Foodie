import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchRecipesStart } from "../../store/recipe/recipe.action";
import { selectRecipes } from "../../store/recipe/recipe.selector";
import { FeaturedContainer, FeaturedImage, FeaturedTitle, HomeContainer } from "./home.style";


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
          <FeaturedImage src="images/board-veg.jpg"/>
        </FeaturedContainer>
      </HomeContainer>
   )
}

export default Home;