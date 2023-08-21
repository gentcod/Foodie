import RecipeCardContainer from "../../components/recipe-card-container/recipe-card-container.component.";
import { categoryData } from "../../dev-data/recipe-data";

import {
  Container,
  RecipeCategory,
  RecipeCategoryContent,
  RecipeCategoryHeader,
  RecipeCategoryItem,
  RecipeCategoryItemImage,
  RecipeCategoryItemName,
} from "./recipes.style";

const Recipes = () => {


  return (
    <Container>
      {categoryData.map((data) => (
        <RecipeCategory key={data.id}>
          <RecipeCategoryHeader>{data.heading}</RecipeCategoryHeader>
          <RecipeCategoryContent>
            {data.contents.map((el) => (
              <RecipeCategoryItem key={el.id}>
               <RecipeCategoryItemImage src={el.imgSrc}/>
               <RecipeCategoryItemName>{el.name}</RecipeCategoryItemName>
              </RecipeCategoryItem>
            ))}
          </RecipeCategoryContent>
        </RecipeCategory>
      ))}
      <RecipeCardContainer/>
    </Container>
  );
};

export default Recipes;