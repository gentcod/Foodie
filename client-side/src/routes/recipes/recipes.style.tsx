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
   column-gap: 2rem;
   justify-content: center;
`;

export const RecipeCategoryItem = styled.div`
   width: 35rem;
   height: 35rem;
   border-radius: 1rem;
   box-shadow: 1rem 1rem .7rem rgba(0, 0, 0, .3);
`;

export const RecipeCategoryItemImage = styled.img`

`;

export const RecipeCategoryItemName = styled.p`

`;