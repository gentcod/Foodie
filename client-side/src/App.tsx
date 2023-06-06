import { useSelector } from "react-redux/es/exports";
import { selectRecipes } from "./store/recipe/recipe.selector";
import { useEffect } from "react";
import { useDispatch } from "react-redux";

import "./App.css";
import { fetchRecipesStart } from "./store/recipe/recipe.action";

function App() {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRecipesStart());
  }, [dispatch])

  const data = useSelector(selectRecipes);
  console.log(data);
  const {} = data;

  return (
    <div className="App">
      <h1>Welcome!!! This is the frontend</h1>
      <p>Folder Resolution</p>
    </div>
  );
}

export default App;
