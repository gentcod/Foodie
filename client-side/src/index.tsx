import React from "react";
import ReactDOM from "react-dom/client";
import { GlobalStyle } from "./globalStyle";
// import { ClerkProvider } from '@clerk/clerk-react'

import 'leaflet/dist/leaflet.css'
// import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./store/store";
import * as serviceWorkerRegistration from './serviceWorkerRegistration';

if (!process.env.REACT_APP_CLERK_PUBLISHABLE_KEY) {
  throw new Error("Missing Publishable Key")
}
// const clerkPubKey = process.env.REACT_APP_CLERK_PUBLISHABLE_KEY;

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    {/* <ClerkProvider publishableKey={clerkPubKey}> */}
      <Provider store={store}>
        <BrowserRouter>
          <GlobalStyle/>
          <App />
        </BrowserRouter>
      </Provider>
    {/* </ClerkProvider> */}
  </React.StrictMode>
);

serviceWorkerRegistration.register();
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
