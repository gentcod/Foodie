import styled from "styled-components";

export const RecipeContainer = styled.div`
   height: 18rem;
   width: 80%;
   padding: 2rem;
   text-align: left;
   background-color: #555;
   border-radius: 1rem;
   color: white;

   display: flex;
   column-gap: 2rem;
   align-items: center;
`;

export const RecipeInnerLeft = styled.div`

`;

export const RecipeInnerRight = styled.div`
   display: flex;
   flex-direction: column;
   row-gap: 1rem;
`;

export const RecipeImage = styled.img`
   height: 15rem;
   width: 15rem;
   border-radius: 50%;
`;

export const RecipeName = styled.h4`

`;

export const RecipeCookTime  = styled.p`

`;

export const RecipeDescription = styled.p`

`;

export const RecipeOrigin = styled.p`

`;