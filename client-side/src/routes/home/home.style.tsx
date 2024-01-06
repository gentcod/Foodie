
import styled from "styled-components";

export const HomeContainer = styled.div`
   padding: 2rem 5rem;
   background-image: url(https://i.ibb.co/rGK2CC5/foreign.webp);
   background-attachment: fixed;
   background-position: center;
`

export const FeaturedContainer = styled.div`
   width: 100%%;
   margin: 0 auto;
   margin-top: 3rem;
   border-radius: 1rem;
`

export const FeaturedTitle = styled.p`
   font-weight: 300;
   font-size: 1.4rem;
   text-align: left;
   text-transform: uppercase;
   margin-bottom: 2rem;
`

export const FeaturedImage = styled.img`
   width: 100%;
   height: 30rem;
   object-fit: cover;
   object-position: center;
   border-radius: 2rem;
`

export const WelcomeContainer = styled.div`
   height: 25rem;
   width: 80%;
   margin: 0 auto;
   padding: 3rem;
   border-radius: 1rem;
   margin-top: 3rem;
   display: flex;
   column-gap: 5rem;
   justify-content: center;
   align-items: center;
   background-color: #333;
`;

export const WelcomeLogo = styled.img`
   width: 20rem;
   height: 20rem;
   border-radius: 50%;
`;

export const WelcomeText = styled.p`
   width: 50%;
   font-size: 1.4rem;
   font-weight: 500;
   color: #e6be8a;
`;