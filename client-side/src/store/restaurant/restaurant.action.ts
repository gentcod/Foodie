import { createAction, Action, ActionWithPayload, withMatcher } from "../../utils/reducer/reducer.utilities";
import { RESTAURANT_ACTION_TYPES } from "./restaurant.types";
import { Restaurant } from "../../app/models/restaurant";

export type FetchRestaurantsStart = Action<RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_START>;
export type FetchRestaurantsSuccess = ActionWithPayload<RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_SUCCESS, Restaurant[]>;
export type FetchRestaurantsFailed = ActionWithPayload<RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_FAILED, Error>;

export const fetchRestaurantsStart = withMatcher((): FetchRestaurantsStart => createAction(RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_START));
export const fetchRestaurantsSuccess = withMatcher((restaurant: Restaurant[]): FetchRestaurantsSuccess => createAction(RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_SUCCESS, restaurant));
export const fetchRestaurantsFailed = withMatcher((error: Error): FetchRestaurantsFailed => createAction(RESTAURANT_ACTION_TYPES.FETCH_RESTAURANT_FAILED, error));