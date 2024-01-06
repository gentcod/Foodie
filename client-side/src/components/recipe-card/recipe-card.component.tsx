import  {ReactComponent as TimeIcon } from '../../assets/alarm.svg';

import { RecipeContainer, RecipeCookTime, RecipeDescription, RecipeImage, RecipeImageContainer, RecipeInnerLeft, RecipeContentContainer, RecipeName, RecipeOrigin, RecipeIconContents } from './recipe-card.style';

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
            <RecipeImageContainer>
               <RecipeImage src={imgSrc}/>
            </RecipeImageContainer>
         <RecipeContentContainer>
            <RecipeName>{name}</RecipeName>
            <RecipeOrigin>{origin}</RecipeOrigin>
            {/* <RecipeDescription>{description}</RecipeDescription> */}
            <RecipeIconContents>
               <RecipeCookTime>{cookTime}</RecipeCookTime>
               <TimeIcon/>
            </RecipeIconContents>
         </RecipeContentContainer>
      </RecipeContainer>
   )
}

export default RecipeCard;