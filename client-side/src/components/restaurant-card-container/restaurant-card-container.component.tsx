import { CardContainer } from "./restaurant-card-container.style";
import { useEffect } from 'react';
import { selectRestaurants, selectRestaurantsIsLoading } from '../../store/restaurant/restaurant.selector';
import { useSelector, useDispatch } from 'react-redux';
import { fetchRestaurantsStart } from '../../store/restaurant/restaurant.action';
import RestaurantCard from "../restaurant-card/restaurant-card.component";
import LoadingComp from "../loading-comp/loading-comp.component";
import Loading from "../loading/loading.component";

const RestaurantCardContainer = () => {
   const dispatch = useDispatch();

   useEffect(() => {
      dispatch(fetchRestaurantsStart());
   }, [dispatch]);

   const restaurants = useSelector(selectRestaurants);
   const isLoading = useSelector(selectRestaurantsIsLoading)
   console.log(restaurants)

   return (
      <CardContainer>
         {
            isLoading ?
            <LoadingComp/> :
            restaurants.map(el => (
               isLoading ? <Loading/> : <RestaurantCard key={el.id} name={el.name} location={el.location} imgSrc={el.imgSrc}/>
            ))
         }
      </CardContainer>
   )
}

export default RestaurantCardContainer;