import { combineReducers } from "redux";

import authReducer from "./authReducer";
import langReducer from "./langReducer";

const rootReducer = combineReducers({
  auth: authReducer,
  lang: langReducer,
});

export default rootReducer;
