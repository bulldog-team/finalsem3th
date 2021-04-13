import { Dispatch } from "redux";

import {
  IUserData,
  localStorageService,
} from "../../helper/localStorage/localStorageService";
import authApi from "../../helper/axios/authApi";
import {
  AUTH_FAILED,
  AUTH_LOGOUT,
  AUTH_START,
  AUTH_SUCCESS,
  AppAction,
} from "./actionType";

export const authStart = (): AppAction => ({
  type: AUTH_START,
  loading: false,
});

export const authSuccess = (userData: IUserData): AppAction => {
  const array = userData.role?.map((item) => item);
  return {
    type: AUTH_SUCCESS,
    username: userData.username,
    token: userData.token,
    email: userData.email,
    userId: userData.userId,
    role: array,
    loading: false,
    error: null,
  };
};

export const authFailed = (err: string): AppAction => ({
  type: AUTH_FAILED,
  error: err,
  loading: false,
});

export const authLogout = (): AppAction => {
  localStorageService.clearAll();
  return {
    type: AUTH_LOGOUT,
    loading: false,
  };
};

export const handleAutoLogin = () => {
  return (dispatch: Dispatch<AppAction>) => {
    const token = localStorageService.getToken();
    if (!token) {
      return dispatch(authLogout());
    }
    const userData: IUserData = localStorageService.getUserData();
    dispatch(authSuccess(userData));
  };
};

export const handleLogin = (username: string, password: string) => {
  return async (dispatch: Dispatch<AppAction>) => {
    try {
      dispatch(authStart());
      const userResponse = await authApi.login(username, password);
      const user: IUserData = userResponse.data;
      localStorageService.setUserData(user);
      dispatch(authSuccess(user));
    } catch (error) {
      dispatch(authFailed(error.response.data));
    }
  };
};
