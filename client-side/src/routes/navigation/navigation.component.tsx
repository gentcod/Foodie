import { Outlet } from 'react-router-dom';
import { NavigationContainer, NavigationItem, NavigationItemIcon, NavigationItemsContainer, SearchItem} from './navigation.style';
import Search from '../../components/search/search.component';
import { useState } from 'react';
import { navItemsLeft, navItemsRight } from '../../dev-data/navigation-data';

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
         </NavigationItemsContainer>
         {showSearch && <Search/>}
      </NavigationContainer>
      <Outlet/>
      </>
   )
}

export default Navigation;