import {all, call} from 'redux-saga/effects';
import { recipesSaga } from './recipe/recipe.saga';


export function* rootSaga() {
   yield all([call(recipesSaga)]);
}