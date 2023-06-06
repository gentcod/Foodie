import { combineReducers } from 'redux';
import { recipesReducer } from './recipe/recipe.reducer';

export const rootReducer = combineReducers({
   recipes: recipesReducer,
})