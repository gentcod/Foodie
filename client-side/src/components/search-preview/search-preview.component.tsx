import { Button, Container, Recipe, RecipeDetail, RecipeDetailsContainer, RecipeImage, RecipesContainer } from './search-preview.style';
import { useDispatch, useSelector } from "react-redux";
import { selectRecipesSearch, selectSearchParams } from '../../store/recipe/recipe.selector';
import { useEffect } from 'react';
import { fetchRecipesSearchStart } from '../../store/recipe/recipe.action';

type SearchProps = {
   searchString: string
}

const SearchPreview = ({searchString}: SearchProps) => {
   const dispatch = useDispatch();

   useEffect(() => {
      dispatch(fetchRecipesSearchStart(searchString))
   }, [dispatch, searchString])
   const searchRecipes = useSelector(selectRecipesSearch)
   const searchParams = useSelector(selectSearchParams)

   return (
      <Container>
         <RecipesContainer>
            {searchRecipes.map(rec => 
            <Recipe to={""} key={rec.id}>
               <RecipeImage src={rec.imageSrc}/>
               <RecipeDetailsContainer>
                  <RecipeDetail>{rec.name}</RecipeDetail>
               </RecipeDetailsContainer>
            </Recipe>)}
         </RecipesContainer>
         <Button to={`/recipes${searchParams}`}>View All</Button>
      </Container>
   )
};

export default SearchPreview;