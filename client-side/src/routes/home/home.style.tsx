
import styled from "styled-components";

export const HomeContainer = styled.div`
   padding: 2rem 5rem;
`

export const FeaturedContainer = styled.div`
   width: 100%%;
   margin: 0 auto;
   margin-top: 3rem;
   border-radius: 1rem;
   // padding: 2rem 5rem;
   // border: 1px solid #222;
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
   height: 40rem;
   object-fit: cover;
   object-position: center;
   border-radius: 2rem;
`

export const WelcomeContainer = styled.div`
   height: 30rem;
   padding: 3rem;
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
   font-size: 1.6rem;
   font-weight: 500;
   // color: #333;
`;