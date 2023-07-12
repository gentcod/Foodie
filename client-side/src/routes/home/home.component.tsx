import Map from "../../components/map/map.component";

import { FeaturedContainer, FeaturedImage, FeaturedTitle, HomeContainer } from "./home.style";
import RecipeCategoryContainer from "../../components/recipe-category-container/recipe-category-container.component";


const Home = () => {

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