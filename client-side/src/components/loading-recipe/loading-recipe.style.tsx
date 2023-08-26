import styled from "styled-components";

export const Container = styled.div`
   display: flex;
   flex-direction: column;
`;

export const LoadingContainer = styled.div`
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

export const ContainerInnerLeft = styled.div`

`;

export const ContainerInnerRight = styled.div`
   display: flex;
   flex-direction: column;
   row-gap: 1rem;
`;

export const ContainerName = styled.div`
   background-color: #777;
`;

export const ContainerCookTime  = styled.div`
background-color: #777; 
`;

export const ContainerDescription = styled.div`
background-color: #777;
`;

export const ContainerOrigin = styled.div`
background-color: #777;
`;