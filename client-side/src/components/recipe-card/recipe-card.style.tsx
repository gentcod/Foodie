import styled from "styled-components";

export const RecipeImageContainer = styled.div`
   height: 10rem;
   width: 10rem;
   border-radius: 30%;
   object-fit: cover;
   border: 1px solid #e6be8a;
   overflow: hidden;
   backface-visibility: hidden;
   position: absolute;
   top: -3rem;
   left: 50%;
   transform: translate(-50%, 0);
   box-shadow: .5rem .5rem 1rem rgba(0,0,0,.5);
`;

export const RecipeImage = styled.img`
   height: 100%;
   width: 100%;
   object-fit: cover;
   transition: .8s all ease;
`;

export const RecipeContainer = styled.div`
   height: 25rem;
   width: 25rem;
   padding: 2rem;
   text-align: left;
   background-color: #a1a1a1;
   border-radius: 1rem;
   color: #555;
   cursor: pointer;
   transition: .8s all ease;
   backface-visibility: hidden;
   position: relative;

   display: flex;
   column-gap: 3rem;
   align-items: end;

   &:hover ${RecipeImage} {
      transform: scale(1.2);
   }
`;

export const RecipeInnerLeft = styled.div`
   padding: .5rem;
`;

export const RecipeContentContainer = styled.div`
   height: 15rem;
   width: 100%;
   display: flex;
   flex-direction: column;
   justify-content: center;
   align-contents: center;
   row-gap: .5rem;
   padding: .5rem;
`;

export const RecipeContent = styled.p`
   width: 100%;
   padding: 2px;
   text-align: center;
   font-weight: 500;
   background-color: rgba(230, 190, 138, .5);
   border-radius: 3px;
   border: 1px solid white;
   box-shadow: .5rem .5rem 1rem rgba(0,0,0,.5);
`;

export const RecipeName = styled(RecipeContent)`
   font-size: 1.8rem;
   color: #000;
   font-weight: 1000;
   margin-bottom: 1.5rem;
`;

export const RecipeCookTime  = styled(RecipeContent)`
   width: 50%;
`;

export const RecipeDescription = styled(RecipeContent)`

`;

export const RecipeOrigin = styled(RecipeContent)`

`;

export const RecipeIconContents = styled.div`
   width: 100%;
   display: flex;
   flex-direction: row;
   justify-content: center;
   align-contents: center;
   align-items: center;
`;