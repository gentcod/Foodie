import styled from "styled-components";

export const RecipeImageContainer = styled.div`
   height: 15rem;
   width: 15rem;
   border-radius: 1rem;
   object-fit: cover;
   border: 1px solid #e6be8a;
   overflow: hidden;
   backface-visibility: hidden;
`;

export const RecipeImage = styled.img`
   height: 100%;
   width: 100%;
   object-fit: cover;
   transition: .8s all ease;
`;

export const RecipeContainer = styled.div`
   height: 18rem;
   width: 60%;
   padding: 2rem;
   text-align: left;
   background-color: #555;
   border-radius: 1rem;
   color: white;
   cursor: pointer;
   transition: .8s all ease;
   backface-visibility: hidden;

   display: flex;
   column-gap: 3rem;
   align-items: center;

   &:hover ${RecipeImage} {
      transform: scale(1.2);
   }
`;

export const RecipeInnerLeft = styled.div`
   padding: .5rem;
`;

export const RecipeInnerRight = styled.div`
   display: flex;
   width: 60%;
   flex-direction: column;
   row-gap: 1rem;
   padding: .5rem;
`;

export const RecipeContent = styled.p`
   width: 100%;
   padding: 2px;
   margin-left: 1rem;
   background-color: rgba(230, 190, 138, .5);
   border-radius: 3px;
   border: 1px solid white;
`;

export const RecipeName = styled(RecipeContent)`

`;

export const RecipeCookTime  = styled(RecipeContent)`

`;

export const RecipeDescription = styled(RecipeContent)`

`;

export const RecipeOrigin = styled(RecipeContent)`

`;