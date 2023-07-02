import {Routes, Route} from 'react-router-dom';

import "./App.css";
// import "./sass/main.scss";
import Navigation from "./routes/navigation/navigation.component";
import Home from "./routes/home/home.component";
import Recipes from './routes/recipes/recipes.component';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route element={<Navigation/>}>
          <Route path="/" index element={<Home/>}/>
          <Route path='recipes' element={<Recipes/>}/>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
