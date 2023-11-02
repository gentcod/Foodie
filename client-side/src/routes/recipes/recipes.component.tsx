import { categoryData } from "../../dev-data/recipe-page-data";
import Heading from "../../components/heading/heading.component";

import {
  Container,
  RecipeCategory,
  RecipeCategoryContent,
  RecipeCategoryItem,
  RecipeCategoryItemImage,
  RecipeCategoryItemName,
} from "./recipes.style";
import { Outlet } from "react-router-dom";

const Recipes = () => {
  return (
    <>
      <Container>
        {categoryData.map((data) => (
          <RecipeCategory key={data.id}>
            <Heading text={data.heading} />
            <RecipeCategoryContent>
              {data.contents.map((el) => (
                <RecipeCategoryItem key={el.id}>
                  <RecipeCategoryItemImage src={el.imgSrc} />
                  <RecipeCategoryItemName>{el.name}</RecipeCategoryItemName>
                </RecipeCategoryItem>
              ))}
            </RecipeCategoryContent>
          </RecipeCategory>
        ))}
      </Container>
      <Heading text="Top Rated Recipes"/>
      
      <Outlet />
    </>
  );
};

export default Recipes;
