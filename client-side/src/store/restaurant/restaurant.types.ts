export enum RESTAURANT_ACTION_TYPES {
   FETCH_RESTAURANT_START = 'recipe/FETCH_RESTAURANT_START',
   FETCH_RESTAURANT_SUCCESS = 'recipe/FETCH_RESTAURANT_SUCCESS',
   FETCH_RESTAURANT_FAILED = 'recipe/FETCH_RESTAURANT_FAILED',
};

export type Bearing = {
   latitude: number,
   longitude: number
}

export type Rating = {
   ratingNum: number,
   comment: string,
}

export type Restaurant = {
   id: number,
   name: string,
   location: string,
   geolocation: Bearing,
   rating: Rating
}