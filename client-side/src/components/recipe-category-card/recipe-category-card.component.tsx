import { Container, RecipeCategoryContent, RecipeCategoryContentContainer, RecipeCategoryDescription, RecipeCategoryName, RecipeImage } from './recipe-category-card.style';

export type CardProps = {
   name: string;
   description: string;
   imgSrc: string;
   link: string;
}

const RecipeCategoryCard = ({name, description, imgSrc, link}: CardProps) => {
   return (
      <Container to={link}>
         <RecipeImage src={imgSrc}/>
         <RecipeCategoryContentContainer>
            <RecipeCategoryContent>
               <RecipeCategoryName>{name}</RecipeCategoryName>
               <RecipeCategoryDescription>{description}</RecipeCategoryDescription>
            </RecipeCategoryContent>
         </RecipeCategoryContentContainer>
      </Container>
   )
}

export default RecipeCategoryCard;