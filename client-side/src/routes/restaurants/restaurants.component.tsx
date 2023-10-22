import Heading from '../../components/heading/heading.component';
import RestaurantCardContainer from '../../components/restaurant-preview/restaurant-preview.component';
import {  RestaurantsContainer } from './restaurants.style';


const Restaurants = () => {

   return (
      <RestaurantsContainer>
            <Heading text='Listed Restaurants'/>
         <RestaurantCardContainer/>
      </RestaurantsContainer>
   )
};

export default Restaurants;