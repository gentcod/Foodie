import styled from "styled-components";
import { Link } from "react-router-dom";

export const RecipeImage = styled.img`
   width: 100%;
   height: 100%;
   object-fit: cover;
   transition: .8s all ease;
`;

export const RecipeCategoryContent = styled.div`
   height: 10rem;
   padding: 2rem;
   background-color: #555;
   color: #e8e8e8;
   border-radius: .5rem;
   text-align: left;
   transition: .8s all ease;

   display: flex;
   flex-direction: column;
   row-gap: 1rem;
`;

export const Container = styled(Link)`
   height: 25rem;
   width: 15rem;
   border: 1px soild #333;
   border-radius: .5rem;
   overflow: hidden;
   position: relative;
   cursor: pointer;
   transition: .8s all ease;

   box-shadow: 1rem 1rem .7rem rgba(0, 0, 0, .3);

   flex: 0 0 25%;

   &:hover ${RecipeImage}{
      transform: translateY(-1rem) scale(1.2);
   }

   &:hover ${RecipeCategoryContent} {
      color: #e6be8a;
   }
`;

export const RecipeCategoryContentContainer = styled.div`
   width: 90%;
   position: absolute;
   top: 50%;
   left: 50%;
   transform: translate(-50%, 0);
`

export const RecipeCategoryName = styled.h3`

`;

export const RecipeCategoryDescription = styled.p`

`;