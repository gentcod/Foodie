import { data } from "../../dev-data/recipe-category-data";
import RecipeCategoryCard from "../recipe-category-card/recipe-category-card.component";
import { Container } from "./recipe-category-container.style";

const RecipeCategoryContainer = () => {
  return (
    <Container>
      {data.map((el) => (
        <RecipeCategoryCard
          key={el.id}
          name={el.name}
          description={el.description}
          imgSrc={el.imgSrc}
          link={el.link}
        />
      ))}
    </Container>
  );
};

export default RecipeCategoryContainer;
