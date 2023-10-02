import { Link } from "react-router-dom";
import styled from "styled-components";

export const DropdownContainer = styled.div`
   height: 15rem;
   width: 25rem;
   background-color: #555;
   padding: 1rem;
   border-radius: 1rem;
   box-shadow: .6rem .6rem 1rem rgba(0, 0, 0, .3);
   position: absolute;
   right: 13rem;
   top: 13rem;
   z-index: 10;

   display: flex;
   flex-direction: column;
`;

export const UserDetailsContainer = styled.div`
   height: 5rem;
   background-color: #e6be8a;
   border-radius: 3rem;

   display: flex;
   column-gap: 2rem;
   justify-content: center;
   align-items: center;
`;

export const UserPicture = styled.img`
   height: 4rem;
   width: 4rem;
   border-radius: 50%;
   overflow: hidden;
   object-fit: cover;
`;

export const UserName = styled.h5`
   color: #444;
   font-size: 1.4rem;
   letter-spacing: 1px;
`;

export const UserModList = styled.ul`
   text-align: left;
   padding: 1rem;
   margin-left: 2rem;

   display: flex;
   flex-direction: column;
`;

export const UserModListItem = styled(Link)`
   text-transform: capitalize;
   font-weight: 500;
   letter-spacing: 1px;
   padding: 5px 0;
   color: white;

   &:hover {
      color: #e6be8a;
   }
`;