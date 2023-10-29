import { combineReducers } from 'redux';
import { recipesRatingsReducer, recipesReducer, } from './recipe/recipe.reducer';
import { restaurantReducer } from './restaurant/restaurant.reducer';

export const rootReducer = combineReducers({
   recipes: recipesReducer,
   recipesRatings: recipesRatingsReducer,
   restaurants: restaurantReducer,
   // recipeSearch: recipesSearchReducer,
})