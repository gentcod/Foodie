import { Outlet } from 'react-router-dom';
import { useState } from 'react';
import { navItemsLeft, navItemsRight } from '../../dev-data/navigation-data';

import Search from '../../components/search/search.component';
import UserDropdown from '../../components/user-dropdown/user-dropdown.component';

import { Container, NavigationContainer, NavigationItem, NavigationItemIcon, NavigationItemsContainer, NavigationItemsContainerRight, SearchItem, UserProfile} from './navigation.style';
import Header from '../../components/header/header.component';

const Navigation = () => {
   const [hideSearch, setHideSearch] = useState(true);
   const [showSearch, setShowSearch] = useState(false);

   const [hideProfile, setHideProfile] = useState(true);
   const [showProfile, setShowProfile] = useState(false);

   const changeSearchState = () => {

      if (hideSearch === true) 
      {
         setShowSearch(true);
         setHideSearch(false);
         setShowProfile(false);
      }

      else {
         setShowSearch(false);
         setHideSearch(true);
         setHideProfile(true)
      }
   }

   const changeProfileState = () => {

      if (hideProfile === true) 
      {
         setShowProfile(true);
         setHideProfile(false);
         setShowSearch(false);
      }

      else {
         setShowProfile(false);
         setHideProfile(true);
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
            <NavigationItemsContainerRight>
               {
                  navItemsRight.map(item => item.title === 'search' ?
                  <SearchItem to={'#'} key={item.id} onClick={changeSearchState}>
                     <span>{item.title}</span>
                     <NavigationItemIcon src={item.icon}/>
                  </SearchItem>
               :
                  (
                     item.title === 'user' ? 
                     <UserProfile key={item.id} to={'#'} onClick={changeProfileState}>
                        <span>{item.title}</span>
                        <NavigationItemIcon src={item.icon}/>
                     </UserProfile>
                     :
                     <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>
                        <span>{item.title}</span>
                        <NavigationItemIcon src={item.icon}/>
                     </NavigationItem>
                  )
                  )
               }
            </NavigationItemsContainerRight>
            {showProfile && <UserDropdown name='Oyefule Oluwatayo' imgSrc='icons/user-profile.svg'/>}
            {showSearch && <Search/>}
         </NavigationContainer>
      </Container>
      <Outlet/>
      </>
   )
}

export default Navigation;