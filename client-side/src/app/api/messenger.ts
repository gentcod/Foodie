import axios, { AxiosError, AxiosResponse } from "axios";

const sleep = () => new Promise(resolve => setTimeout(resolve, 500));

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(async response => {
   if (process.env.NODE_ENV === 'development') await sleep();
   return response;
}, (error: AxiosError) => {
   const { data, status } = error.response as AxiosResponse;
   switch (status) {
      case 400:
         if (data.errors) {
            const modelStateErrors: string[] = [];
            for (const key in data.errors) {
               if (data.erros[key]) modelStateErrors.push(data.errors[key])
            }

            throw modelStateErrors.flat();
         }
         console.error(data.title)
         break;
   }

   return Promise.reject(error.response);
});

const request = {
   get: (url: string, params?: URLSearchParams) => axios.get(url, {params}).then(responseBody),
   post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
   put: (url: string, body: {}) => axios.put(url, body).then(responseBody)
}

const Recipes = {
   list: (params?: URLSearchParams) => request.get('recipe',params),
   addRating: (recipeId: URLSearchParams, rating: number, review: string) => 
      request.put(`recipe/addRating?recipeId=${recipeId}`, {
         'ratingNum': rating,
         'comment': review,
      }
   ),
   listRecipeRatings: () => request.get('recipeRatings/')
}

const Restaurant = {
   list: (params?: URLSearchParams) => request.get('restaurant',params),
   addRating: (resaturantId: number, rating: number, review: string) => 
      request.put(`recipe/addRating?resaturantId=${resaturantId}`, {
         'ratingNum': rating,
         'comment': review,
      }),
}

const Bookmarks = {
   list: (params: URLSearchParams) => request.get('bookmarks', params),
   addBookMark: () => request.post(``, {})
}

const Favorites = {
   list: (params: URLSearchParams) => request.get('favorite', params),
   addFavoriteRecipe: (userId: string, recipeId: number) => request.post(`favorites/AddRecipe?userId=${userId}&recipeId=${recipeId}`, {}),
   addFavoriteRestaurant: (userId: string, resaturantId: number) => request.post(`favorites/AddRestaurant?userId=${userId}&resaturantId=${resaturantId}`, {})
}

const messenger = {
   Recipes,
   Restaurant,
   Bookmarks,
   Favorites,
}

export default messenger;