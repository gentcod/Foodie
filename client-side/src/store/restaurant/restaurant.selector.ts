import { createSelector } from "reselect";
import { RestaurantState } from "./restaurant.reducer";

const selectRestaurantReducer = (state: any): RestaurantState => state.restaurants;

export const selectRestaurants = createSelector(
   [selectRestaurantReducer],
   (restaurantSlice) => restaurantSlice.restaurants
)

export const selectRestaurantsIsLoading = createSelector(
   [selectRestaurantReducer],
   (restaurantSlice) => restaurantSlice.isLoading
)