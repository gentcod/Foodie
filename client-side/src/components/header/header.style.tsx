import { Link } from "react-router-dom";
import styled from "styled-components";

export const Container = styled(Link)`
   height: 8rem;
   width: auto;
   padding: 1rem;

   display: flex;
   justify-content: center;
   align-items: center;
   column-gap: 3rem;
`;

export const HeaderImage = styled.img`
   height: 90%;
   object-fit: cover;
`;

export const HeaderTitle = styled.h1`
   font-size: 4rem;
   font-family: 'Rubik Iso', cursive;
   color: white;
`;