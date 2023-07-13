import { Outlet } from 'react-router-dom';
import { NavigationContainer, NavigationItem, NavigationItemIcon, NavigationItemsContainer} from './navigation.style';
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
               navItemsRight.map(item => 
               <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>
                  <NavigationItemIcon src={item.icon}/>
               </NavigationItem>)
            }
         </NavigationItemsContainer>
         <Search/>
      </NavigationContainer>
      <Outlet/>
      </>
   )
}

export default Navigation;