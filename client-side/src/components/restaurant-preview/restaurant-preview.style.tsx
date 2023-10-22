import styled from "styled-components";

const backgroundImage = 'https://i.ibb.co/GnrtwQ3/jay-wennington-N-Y88-TWm-Gw-A-unsplash.jpg'

export const CardContainer = styled.div`
   height: 100%;
   padding: 3rem;
   display: flex;
   flex-direction: column;
   row-gap: 2rem;
   justify-content: center;
   align-items: center;
   backface-visibility: hidden;
   background-image: url(${backgroundImage});
   background-position: center;
   border-radius: .5rem;
   margin-top: 1rem;
`;