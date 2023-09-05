import styled from "styled-components";

export const RestaurantImageContainer = styled.div`
   height: 15rem;
   width: 15rem;
   border-radius: 1rem;
   object-fit: cover;
   border: 1px solid #555;
   overflow: hidden;
   backface-visibility: hidden;
`;

export const RestaurantImage = styled.img`
   height: 100%;
   width: 100%;
   object-fit: cover;
   transition: .8s all ease;
`;

export const RestaurantContainer = styled.div`
   height: 18rem;
   width: 60%;
   padding: 2rem;
   text-align: left;
   background-color: rgba(230, 190, 138, .5);;
   border-radius: 1rem;
   color: white;
   cursor: pointer;
   transition: .8s all ease;
   backface-visibility: hidden;

   display: flex;
   column-gap: 3rem;
   align-items: center;

   &:hover ${RestaurantImage} {
      transform: scale(1.2);
   }
`;

export const RestaurantInnerLeft = styled.div`
   padding: .5rem;
`;

export const RestaurantInnerRight = styled.div`
   display: flex;
   width: 60%;
   flex-direction: column;
   row-gap: 1rem;
   padding: .5rem;
`;

export const RestaurantContent = styled.p`
   width: 100%;
   padding: 2px;
   margin-left: 1rem;
   background-color: #555;
   border-radius: 3px;
   border: 2px solid #222;
`;