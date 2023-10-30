import styled from "styled-components";

export const Container = styled.div`
   display: flex;
   flex-direction: column;
   min-height: 8rem;
   max-height: 30rem;
   width: 50rem;
   background-color: white;
   border-radius: .5rem;

   position: relative;
`

export const Button = styled.button`
   width: 90%;
   // position: absolute;
   // bottom: 1rem;
   justify-content: flex-end
`;