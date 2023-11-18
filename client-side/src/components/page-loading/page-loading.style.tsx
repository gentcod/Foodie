import styled, { keyframes } from "styled-components";

export const Bounce = keyframes`
   0% {
      transform: translateY(1rem) scale(1.1);
   }

   25% {
      transform: translateY(-1rem) scale(1);
   }

   50% {
      transform: translateY(1rem) scale(1.2);
   }

   75% {
      transform: translateY(-1rem) scale(1.1);
   }

   100% {
      transform: translateY(1rem) scale(1);
   }
`

export const Overlay = styled.div`
   height: 90%;
   width: 90%;
   background-color: rgba(235, 88, 20, .5);
   backdrop-filter: blur(3px);
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

   animation: ${Bounce} 1s ease infinite;
`;

