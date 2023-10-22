import { all, call, put, takeLatest } from "typed-redux-saga/macro";
import messenger from '../../app/api/messenger';
import { RESTAURANT_ACTION_TYPES } from "./restaurant.types";
import { fetchRestaurantsFailed, fetchRestaurantsSuccess } from "./restaurant.action";

const { Restaurant } = messenger;

export const fetchRestaurantsFromApi = async () => {
   const response = await Restaurant.list();
   return response;
}

export function* fetchRestaurantsAsync() {
   try {
      const restaurants = yield* call(fetchRestaurantsFromApi);
      yield* put(fetchRestaurantsSuccess(restaurants));
   } catch (error) {
      yield* put(fetchRestaurantsFailed(error as Error));
   }
}

export function* onfetchRestaurant() {
   yield* takeLatest(RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_START, fetchRestaurantsAsync);
}

export function* restaurantSaga() {
   yield* all([call(onfetchRestaurant)]);
}