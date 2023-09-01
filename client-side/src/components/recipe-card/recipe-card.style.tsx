import styled from "styled-components";

export const RecipeContainer = styled.div`
   height: 18rem;
   width: 70%;
   padding: 2rem;
   text-align: left;
   background-color: #555;
   border-radius: 1rem;
   color: white;

   display: flex;
   column-gap: 3rem;
   align-items: center;
`;

export const RecipeInnerLeft = styled.div`
   padding: .5rem;
`;

export const RecipeInnerRight = styled.div`
   display: flex;
   flex-direction: column;
   row-gap: 1rem;
`;

export const RecipeImage = styled.img`
   height: 14rem;
   width: 15rem;
   border-radius: 1rem;
   object-fit: cover;
   border: 1px solid #e6be8a;
`;

export const RecipeContent = styled.p`
   width: 100%;
   padding-bbtom: .5rem;
   margin-left: 1rem;
   border-bottom: 2px solid #e6be8a;
`;

export const RecipeName = styled(RecipeContent)`
`;

export const RecipeCookTime  = styled(RecipeContent)`

`;

export const RecipeDescription = styled(RecipeContent)`

`;

export const RecipeOrigin = styled(RecipeContent)`

`;