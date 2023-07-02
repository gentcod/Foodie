import styled from "styled-components";

export const NavigationContainer = styled.div`
   height: 4rem;
   width: 100%;
   background-color: #222;
   display: flex;
   position: fixed;

   z-index: 100;
`

export const NavigationItemsContainer = styled.div`
   display: flex;
   width: 30rem;
   padding: 1rem;
`

export const NavigationItem = styled.div`
   width: 100%;
   color: white;
   text-transform: uppercase;
   font-weight: 1000;
`