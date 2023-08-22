import styled from "styled-components";

export const Overlay = styled.div`
   height: 90%;
   width: 90%;
   background-color: rgba(255, 255, 255, .9);
   border-radius: 1rem;
   position: absolute;
   top: 50%;
   left: 50%;
   transform: translate(-50%, -50%);
   z-index: 5000;

   display: flex;
   justify-content: center;
   align-items: center;
`;

export const IconContainer = styled.div`
   margin: 0 auto;


`;

