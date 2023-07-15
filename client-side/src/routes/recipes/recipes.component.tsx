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
    </Container>
  );
};

export default Recipes;

 // const dispatch = useDispatch();
  // useEffect(() => {
  //   dispatch(fetchRecipesStart());
  // }, [dispatch])

  // const data = useSelector(selectRecipes);
  // console.log(data);