import { Outlet } from 'react-router-dom';
import { NavigationContainer} from './navigation.style';

const Navigation = () => {
   return (
      <>
      <NavigationContainer></NavigationContainer>
      <Outlet/>
      </>
   )
}

export default Navigation;