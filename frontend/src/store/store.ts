import { applyMiddleware, compose, createStore } from "redux";
import thunk, { ThunkMiddleware } from "redux-thunk";
import { AppAction } from "./action/actionType";
import rootReducer from "./reducer";

// Handle Redux Store
const composeEnhancers =
  (process.env.NODE_ENV === "development"
    ? (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__
    : null) || compose;

export type AppState = ReturnType<typeof rootReducer>;

const store = createStore(
  rootReducer,
  composeEnhancers(
    applyMiddleware(thunk as ThunkMiddleware<AppState, AppAction>)
  )
);

export default store;
