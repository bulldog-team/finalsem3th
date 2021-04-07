import { useTranslation } from "react-i18next";
import { Dispatch } from "redux";
import {
  AppAction,
  CHANGE_LANG_FAILED,
  CHANGE_LANG_START,
  CHANGE_LANG_SUCCESS,
} from "./actionType";

export const changeLanguageStart = (): AppAction => ({
  type: CHANGE_LANG_START,
  error: null,
  loading: true,
});

export const changeLanguageSuccess = (): AppAction => ({
  type: CHANGE_LANG_SUCCESS,
  error: null,
  loading: false,
});

export const changeLanguageFailed = (mess: string): AppAction => ({
  type: CHANGE_LANG_FAILED,
  error: mess,
  loading: false,
});

export const handleChangeLanguage = (lang: string) => {
  return (dispatch: Dispatch<AppAction>) => {
    try {
      dispatch(changeLanguageStart());
      const { i18n } = useTranslation();
      i18n.changeLanguage(lang);
      dispatch(changeLanguageSuccess());
    } catch (error) {
      console.log(error);
      dispatch(changeLanguageFailed("Translation error!"));
    }
  };
};
