import { Routes, Route } from "react-router-dom";
import { lazy, Suspense } from "react";

import Loading from "./components/page-loading/page-loading.component";

const Navigation = lazy(
  () => import("./routes/navigation/navigation.component")
);
const Home = lazy(() => import("./routes/home/home.component"));
const Recipes = lazy(() => import("./routes/recipes/recipes.component"));
const Restaurants = lazy(
  () => import("./routes/restaurants/restaurants.component")
);
const RecipePreview = lazy(
  () => import("./components/recipe-preview/recipe-preview.component")
);

function App() {
  return (
    <Suspense fallback={<Loading />}>
      <Routes>
        <Route element={<Navigation />}>
          <Route path="/" index element={<Home />} />
          <Route path="recipes/" element={<Recipes />}>
            <Route index element={<RecipePreview />} />
            <Route path=":recipeCat" element={<RecipePreview />} />
          </Route>
          <Route path="restaurants" element={<Restaurants />} />
        </Route>
      </Routes>
    </Suspense>
  );
}

export default App;
