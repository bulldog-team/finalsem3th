import * as actionType from "../action/actionType";
import { IChangeLanguageReducer } from "./reducerType";

type ChangeLanguageAction = {
  error: string | null;
  loading: boolean;
  type: string;
};

const initLangState: IChangeLanguageReducer = {
  error: null,
  loading: false,
};

const changeLanguageFailed = (
  state: IChangeLanguageReducer,
  action: ChangeLanguageAction
): IChangeLanguageReducer => ({
  ...state,
  error: action.error,
  loading: action.loading,
});

const changeLanguageStart = (
  state: IChangeLanguageReducer,
  action: ChangeLanguageAction
): IChangeLanguageReducer => ({
  ...state,
  error: null,
  loading: true,
});

const changeLanguageSuccess = (
  state: IChangeLanguageReducer,
  action: ChangeLanguageAction
): IChangeLanguageReducer => ({
  ...state,
  error: null,
  loading: false,
});

const langReducer = (
  state: IChangeLanguageReducer = initLangState,
  action: ChangeLanguageAction
): IChangeLanguageReducer => {
  switch (action.type) {
    case actionType.CHANGE_LANG_FAILED:
      return changeLanguageFailed(state, action);
    case actionType.CHANGE_LANG_START:
      return changeLanguageStart(state, action);
    case actionType.CHANGE_LANG_SUCCESS:
      return changeLanguageSuccess(state, action);
    default:
      return state;
  }
};

export default langReducer;
