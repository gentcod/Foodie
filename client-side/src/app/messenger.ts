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
   get: (url: string, params?: URLSearchParams) => axios.get(url, {params}).then(responseBody)
}

const Recipes = {
   list: (params?: URLSearchParams) => request.get('recipe',params)
}

const Restaurant = {
   list: (params?: URLSearchParams) => request.get('restaurant',params)
}

const Bookmarks = {
   list: (params: URLSearchParams) => request.get('bookmarks', params)
}

const messenger = {
   Recipes,
   Restaurant,
   Bookmarks,
}

export default messenger;