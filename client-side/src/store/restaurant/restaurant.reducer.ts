import { AnyAction } from "redux";
import { fetchRestaurantsStart, fetchRestaurantsSuccess, fetchRestaurantsFailed } from "./restaurant.action";
import { Restaurant } from "../../app/models/restaurant";

export type RestaurantState = {
   readonly restaurants: Restaurant[];
   readonly isLoading: boolean;
   readonly error?: Error | null;
}

const RESTAURANT_INITIAL_STATE: RestaurantState = {
   restaurants: [],
   isLoading: false,
   error: null,
}

export const restaurantReducer = (state = RESTAURANT_INITIAL_STATE, action = {} as AnyAction) => {
   if (fetchRestaurantsStart.match(action)) {
      return {
         ...state,
         isLoading: true,
      }
   }
   if (fetchRestaurantsSuccess.match(action)) {
      return {
         ...state,
         restaurants: action.payload,
         isLoading: false,
      }
   }
   if (fetchRestaurantsFailed.match(action)) {
      return {
         ...state,
         error: action.payload,
         isLoading: false,
      }
   }
   return state;
}
