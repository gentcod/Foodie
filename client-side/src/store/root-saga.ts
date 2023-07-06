import {all, call} from 'redux-saga/effects';
import { recipesSaga } from './recipe/recipe.saga';
import { restaurantSaga } from './restaurant/restaurant.saga';


export function* rootSaga() {
   yield all([call(recipesSaga), call(restaurantSaga)]);
}