import Map from "../../components/map/map.component";

import {
  FeaturedContainer,
  FeaturedImage,
  FeaturedTitle,
  HomeContainer,
  WelcomeText,
  WelcomeContainer,
  WelcomeLogo,
} from "./home.style";
import RecipeCategoryContainer from "../../components/recipe-category-container/recipe-category-container.component";

const Home = () => {
  return (
    <HomeContainer>
      <FeaturedContainer>
        <FeaturedTitle>Featured recipes</FeaturedTitle>
        <FeaturedImage src="https://i.ibb.co/mhxQWT4/board-veg.jpg" />
      </FeaturedContainer>
      <WelcomeContainer>
        <WelcomeLogo src="images/foodie.png" />
        <WelcomeText>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat. Duis aute irure dolor in
          reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
          pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
          culpa qui officia deserunt mollit anim id est laborum.
        </WelcomeText>
      </WelcomeContainer>
      <RecipeCategoryContainer />
      <Map />
    </HomeContainer>
  );
};

export default Home;
