import Heading from '../../components/heading/heading.component';
import RestaurantCardContainer from '../../components/restaurant-card-container/restaurant-card-container.component';
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