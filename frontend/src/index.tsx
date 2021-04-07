import React, { Suspense } from "react";
import ReactDOM from "react-dom";
import App from "./App";
import reportWebVitals from "./reportWebVitals";

import "./style/main.scss";
import store from "./store/store";
import Loading from "./Component/Loading/Loading";
import { Provider } from "react-redux";

ReactDOM.render(
  // <React.StrictMode>
  <Suspense fallback={<Loading />}>
    <Provider store={store}>
      <App />
    </Provider>
  </Suspense>,
  // </React.StrictMode>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
