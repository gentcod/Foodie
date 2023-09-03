import { RecipeContainer, RecipeCookTime, RecipeDescription, RecipeImage, RecipeImageContainer, RecipeInnerLeft, RecipeInnerRight, RecipeName, RecipeOrigin } from './recipe-card.style';

type RecipeProps = {
   name: string;
   imgSrc: string;
   description: string;
   cookTime: string;
   origin: string;
};

const RecipeCard = ({name, imgSrc, description, cookTime, origin}: RecipeProps) => {
   return (
      <RecipeContainer>
         <RecipeInnerLeft>
            <RecipeImageContainer>
               <RecipeImage src={imgSrc}/>
            </RecipeImageContainer>
         </RecipeInnerLeft>
         <RecipeInnerRight>
            <RecipeName>{name}</RecipeName>
            <RecipeCookTime>{cookTime}</RecipeCookTime>
            <RecipeOrigin>{origin}</RecipeOrigin>
            <RecipeDescription>{description}</RecipeDescription>
         </RecipeInnerRight>
      </RecipeContainer>
   )
}

export default RecipeCard;