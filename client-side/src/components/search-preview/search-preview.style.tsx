import { Link } from "react-router-dom";
import styled from "styled-components";

export const Container = styled.div`
   padding: 1rem;
   display: flex;
   flex-direction: column;
   max-height: 30rem;
   width: 50rem;
   background-color: white;
   border-radius: .5rem;

   position: relative;
`;

export const RecipesContainer = styled.div`
   display: flex;
   flex-direction: column;
   row-gap: .5rem;
   min-height: 4rem;
`;

export const Recipe = styled(Link)`
   border-radius: .5rem;
   width: 85%;
   height: 4rem;
   background-color: rgba(230, 190, 138, 0.5);
   margin: 0 auto;
   padding: .5rem 2rem;

   display: flex;
   justify-content: space-evenly;
   align-items: center;
   transition: .8s all ease;

   &:hover {
      transform: scale(1.02);
   }

   &:active {
      transform: translateY(.5rem);
   }
`;

export const RecipeImage = styled.img`
   height: 3rem;
   width: 3rem;
   object-fit: cover;
   border-radius: 3px;
`;

export const RecipeDetailsContainer = styled.div`
   display: flex;
   flex-direction: column;
   width: 70%;
   font-size: 1.2rem;
   font-weight: 500;
`;

export const RecipeDetail = styled.p`
   text-align: left;
`;

export const Button = styled(Link)`
   text-align: center;
   width: 90%;
   margin: 0 auto;
   margin-top: 1.5rem;
   cursor: pointer;
   padding: .5rem;
   border-radius: .5rem;
   background-color: #ed6b2e;
   color: white;
   font-weight: 700;
   transition: .8s all ease;

   &:hover {
      transform: scale(1.02);
   }

   &:active {
      transform: translateY(.5rem);
   }
`;