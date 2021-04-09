import * as actionType from "../action/actionType";
import { IAuthReducer } from "./reducerType";

type AuthAction = IAuthReducer & {
  type: string;
};

const initState: IAuthReducer = {
  username: null,
  email: null,
  token: null,
  error: null,
  loading: false,
  role: [],
  userId: null,
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
  token: action.token,
  userId: action.userId,
  role: action.role,
  error: null,
  loading: false,
});

const authFailed = (state: IAuthReducer, action: AuthAction): IAuthReducer => ({
  ...state,
  error: action.error,
  loading: false,
});

const authLogout = (state: IAuthReducer, action: AuthAction): IAuthReducer => ({
  ...state,
  username: null,
  email: null,
  token: null,
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
