import { Outlet } from 'react-router-dom';
import { useState } from 'react';
import { navItemsLeft, navItemsRight } from '../../dev-data/navigation-data';

import Search from '../../components/search/search.component';
import UserDropdown from '../../components/user-dropdown/user-dropdown.component';

import { Container, NavigationContainer, NavigationItem, NavigationItemIcon, NavigationItemsContainer, SearchItem} from './navigation.style';
import Header from '../../components/header/header.component';

const Navigation = () => {
   const [hideSearch, setHideSearch] = useState(true);
   const [showSearch, setShowSearch] = useState(false);

   const changeSearchState = () => {

      if (hideSearch === true) 
      {
         setShowSearch(true);
         setHideSearch(false);
      }

      else {
         setShowSearch(false);
         setHideSearch(true);
      }
   }

   return (
      <>
      <Container>
         <Header/>
         <NavigationContainer>
            <NavigationItemsContainer>
               {
                  navItemsLeft.map(item => <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>{item.title}</NavigationItem>)
               }
            </NavigationItemsContainer>
            <NavigationItemsContainer>
               {
                  navItemsRight.map(item => item.title === 'search' ?
                  <SearchItem key={item.id} onClick={changeSearchState}>
                     <NavigationItemIcon src={item.icon}/>
                  </SearchItem>
               :
                  <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>
                     <NavigationItemIcon src={item.icon}/>
                  </NavigationItem>
                  )
               }
               {<UserDropdown name='Oyefule Oluwatayo' imgSrc='icons/user-profile.svg'/>}
            </NavigationItemsContainer>
            {showSearch && <Search/>}
         </NavigationContainer>
      </Container>
      <Outlet/>
      </>
   )
}

export default Navigation;