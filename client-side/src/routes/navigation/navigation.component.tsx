import { Outlet } from 'react-router-dom';
import { NavigationContainer, NavigationItem, NavigationItemsContainer} from './navigation.style';
import Search from '../../components/search/search.component';

type NavItens = {
   id: number;
   title: string;
}[]

const navItems: NavItens = [
   {
      id: 0,
      title: 'recipes'
   },
   {
      id: 1,
      title: 'top rated'
   },
   {
      id: 3,
      title: 'easy makes'
   },
]

const Navigation = () => {
   return (
      <>
      <NavigationContainer>
         <NavigationItemsContainer>
            {
               navItems.map(item => <NavigationItem key={item.id} to={`/${item.title.replace(" ", "")}`}>{item.title}</NavigationItem>)
            }
         </NavigationItemsContainer>
         <Search/>
      </NavigationContainer>
      <Outlet/>
      </>
   )
}

export default Navigation;