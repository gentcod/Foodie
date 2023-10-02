import styled from "styled-components";
import { Link } from "react-router-dom";

export const Container = styled.div`
  display: flex;
  flex-direction: column;
`;

export const NavigationContainer = styled.div`
  height: 4rem;
  width: 100%;
  background-color: #222;
  display: flex;
  justify-content: space-between;
  padding: 0 15rem;
  margin-bottom: 3rem;

  z-index: 100;
`;

export const NavigationItemsContainer = styled.div`
  display: flex;
  width: 35rem;
  padding: 1rem;
  justify-content: center;
  align-items: center;
  column-gap: 3rem;
`;

export const NavigationItemsContainerRight = styled(NavigationItemsContainer)`
  column-gap: 5rem;
`;

export const NavigationItem = styled(Link)`
  color: white;
  text-transform: uppercase;
  font-weight: 1000;
  cursor: pointer;

  display: flex;
  align-items: center;
  column-gap: 1rem;

  &:hover {
    color: #e6be8a;
  }
`;

export const SearchItem = styled(NavigationItem)`
  color: white;
  text-transform: uppercase;
  font-weight: 1000;
`;

export const UserProfile = styled(NavigationItem)`

`

export const NavigationItemIcon = styled.img`

`;
