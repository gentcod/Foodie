import styled from "styled-components";

export const SearchBarContainer = styled.div`
   display: in-line block;
   padding: 1rem;
   position: absolute;
   right: 20rem;
   top: 12rem;
`

export const SearchBar = styled.input`
   border: none;
   outline: none;
   height: 3.5rem;
   width: 30rem;
   border-radius: 1.2rem;
   padding: 1.5rem;
   background-color: #eee;
   color: #555;

   &:focus {
      border-bottom: 3px solid rgb(235, 88, 20);

      ::placeholder {
         color: #333;
         font-weight: 700;
      }
   }
`