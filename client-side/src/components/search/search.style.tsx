import styled from "styled-components";

export const SearchBarContainer = styled.div`
   display: in-line block;
   padding: 1rem;
   position: absolute;
   right: 15rem;
   top: 2.5rem;
`

export const SearchBar = styled.input`
   border: none;
   outline: none;
   height: 3rem;
   width: 30rem;
   border-radius: 1.2rem;
   padding: 1rem;
   background-color: #eee;
   color: #888;

   &:focus {
      border-bottom: 3px solid #333;

      ::placeholder {
         color: #333;
         font-weight: 700;
      }
   }
`