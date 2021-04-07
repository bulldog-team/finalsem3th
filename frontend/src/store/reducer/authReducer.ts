import * as actionType from "../action/actionType";
import { IAuthReducer } from "./reducerType";

type AuthAction = {
  type: string;
  username: string;
  email: string;
  acToken: string;
  rfToken: string;
  error: string;
  loading: boolean;
};

const initState: IAuthReducer = {
  username: null,
  email: null,
  acToken: null,
  rfToken: null,
  error: null,
  loading: false,
};

const authStart = (state: IAuthReducer, action: AuthAction): IAuthReducer => ({
  ...state,
  error: null,
  loading: true,
});

const authSuccess = (
  state: IAuthReducer,
  action: AuthAction
): IAuthReducer => ({
  ...state,
  username: action.username,
  email: action.email,
  acToken: action.acToken,
  rfToken: action.rfToken,
  error: null,
  loading: false,
});

const authFailed = (state: IAuthReducer, action: AuthAction) => ({
  ...state,
  error: action.error,
  loading: false,
});

const authLogout = (state: IAuthReducer, action: AuthAction) => ({
  ...state,
  username: null,
  email: null,
  acToken: null,
  rfToken: null,
  error: null,
  loading: false,
});

const reducer = (
  state: IAuthReducer = initState,
  action: AuthAction
): IAuthReducer => {
  switch (action.type) {
    case actionType.AUTH_START:
      return authStart(state, action);
    case actionType.AUTH_FAILED:
      return authFailed(state, action);
    case actionType.AUTH_LOGOUT:
      return authLogout(state, action);
    case actionType.AUTH_SUCCESS:
      return authSuccess(state, action);
    default:
      return state;
  }
};

export default reducer;
