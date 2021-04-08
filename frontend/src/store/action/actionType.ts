import { IAuthReducer } from "../reducer/reducerType";

export const AUTH_START = "AUTH_START";
export const AUTH_SUCCESS = "AUTH_SUCCESS";
export const AUTH_FAILED = "AUTH_FAILED";
export const AUTH_LOGOUT = "AUTH_LOGOUT";
export const REFRESH_TOKEN = "REFRESH_TOKEN";
export const CHANGE_LANG_START = "CHANGE_LANG_START";
export const CHANGE_LANG_SUCCESS = "CHANGE_LANG_SUCCESS";
export const CHANGE_LANG_FAILED = "CHANGE_LANG_FAILED";

export interface IAuthStart {
  type: typeof AUTH_START;
  loading: boolean;
}

export interface IAuthSucess extends IAuthReducer {
  type: typeof AUTH_SUCCESS;
}

export interface IAuthFailed {
  type: typeof AUTH_FAILED;
  error: string;
  loading: boolean;
}

export interface IAuthLogout {
  type: typeof AUTH_LOGOUT;
  loading: boolean;
}

export type AuthActionType =
  | IAuthStart
  | IAuthSucess
  | IAuthFailed
  | IAuthLogout;

export interface IChangeLanguage {
  type:
    | typeof CHANGE_LANG_START
    | typeof CHANGE_LANG_FAILED
    | typeof CHANGE_LANG_SUCCESS;
  loading: boolean;
  error: null | string;
}

export type ChangeLanguageType = IChangeLanguage;

export type AppAction = AuthActionType | ChangeLanguageType;
