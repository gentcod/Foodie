import styled from "styled-components";

export const Container = styled.div`
   width: 100%;
   display: flex;
   flex-direction: column;
   row-gap: 2rem;
   justify-content: center;
   align-items: center;
`;

export const LoadingContainer = styled.div`
   height: 18rem;
   width: 60%;
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
   padding: .5rem;
   width: 15rem;
   height: 100%;

   position: relative;
`;

export const ContainerInnerRight = styled.div`
   padding: 2rem 0;
   width: 60rem;
   height: 100%;
   display: flex;
   flex-direction: column;
   row-gap: 1rem;
`;

export const ContainerContent = styled.div`
   background-color: #777;
   height: 100%;
   border-radius: .5rem;
`;

export const ContainerContentMid = styled(ContainerContent)`
   width: 15rem;
`;

export const ContainerContentShort = styled(ContainerContent)`
   width: 8rem;
`;
