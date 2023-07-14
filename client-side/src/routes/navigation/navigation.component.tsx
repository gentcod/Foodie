import { Outlet } from 'react-router-dom';
import { NavigationContainer, NavigationItem, NavigationItemIcon, NavigationItemsContainer, SearchItem} from './navigation.style';
import Search from '../../components/search/search.component';

type NavItemsLeft = {
   id: number;
   title: string;
}[]

type NavItemsRight = {
   id: number;
   title: string;
   icon: string;
}[]

const navItemsLeft: NavItemsLeft = [
   {
      id: 0,
      
      title: 'recipes'
   },
   {
      id: 1,
      title: 'restaurants'
   },
   {
      id: 3,
      title: 'easy makes'
   },
]

const navItemsRight: NavItemsRight = [
   {
      id: 0,
      title: 'search',
      icon: 'icons/search.svg'
   },
   {
      id: 1,
      title: 'user',
      icon: 'icons/chef.svg'
   },
   {
      id: 3,
      title: 'bookmarks',
      icon: 'icons/bookmark.svg'
   },
]

const Navigation = () => {
   // const location = useLocation();
   let path = '';

   const setPath = () => {
      path = 'search';
      console.log(path);
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
               <SearchItem key={item.id} onClick={setPath}>
                  <NavigationItemIcon src={item.icon}/>
               </SearchItem>
            :
               <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>
                  <NavigationItemIcon src={item.icon}/>
               </NavigationItem>
               )
            }
         </NavigationItemsContainer>
         {path === 'search' ? <Search/> : null}
      </NavigationContainer>
      <Outlet/>
      </>
   )
}

export default Navigation;