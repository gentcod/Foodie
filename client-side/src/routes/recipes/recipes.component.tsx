import {
  Container,
  RecipeCategory,
  RecipeCategoryContent,
  RecipeCategoryHeader,
  RecipeCategoryItem,
  RecipeCategoryItemImage,
  RecipeCategoryItemName,
} from "./recipes.style";

type RecipeItemProp = {
  id: number;
  name: string;
  imgSrc: string;
};

type RecipeCategoryProp = {
  id: number;
  heading: string;
  contents: RecipeItemProp[];
}[];

const categoryData: RecipeCategoryProp = [
  {
    id: 0,
    heading: "Top Rated Recipes",
    contents: [
      {
        id: 0,
        name: "Grills",
        imgSrc: "",
      },

      {
         id: 1,
         name: "Vegetables",
         imgSrc: "",
       },

       {
         id: 2,
         name: "Rice",
         imgSrc: "",
       },
    ],
  },
];

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