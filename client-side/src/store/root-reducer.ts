import { combineReducers } from 'redux';
import { recipesReducer } from './recipe/recipe.reducer';
import { restaurantReducer } from './restaurant/restaurant.reducer';

export const rootReducer = combineReducers({
   recipes: recipesReducer,
   restaurants: restaurantReducer
})