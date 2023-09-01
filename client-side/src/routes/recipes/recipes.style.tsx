import styled from "styled-components";

export const Container = styled.div`
   padding: 2rem 5rem;
`;

export const RecipeCategory = styled.div`
   width: 100%;
   height: 45rem;
   display: flex;
   flex-direction: column;
   row-gap: 2rem;
`;

export const RecipeCategoryHeader = styled.h2`
   font-weight: 300;
   font-size: 2rem;
   color: #222;
   text-transform: uppercase;
   letter-spacing: .5rem;
   text-align: left;
`;

export const RecipeCategoryContent = styled.div`
   display: flex;
   column-gap: 3rem;
   justify-content: center;
`;

export const RecipeCategoryItem = styled.div`
   width: 30rem;
   height: 35rem;
   overflow: hidden;
   border-radius: 1rem;
   border: 1px solid #e6be8a;
   box-shadow: 1rem 1rem .7rem rgba(0, 0, 0, .3);
   position: relative;
`;

export const RecipeCategoryItemImage = styled.img`
   height: 100%;
   width: 100%;
   object-fit: cover;
   border-radius: 1rem;
`;

export const RecipeCategoryItemName = styled.p`
   height: 10%;
   width: 100%;
   padding: 1rem;
   background-color: rgba(0, 0, 0, .8);
   backdrop-fliter: blur(3px);
   font-weight: 1000;
   text-align: center;
   color: #e6be8a;
   position: absolute;
   bottom: 1rem;
   left: 0;
`;