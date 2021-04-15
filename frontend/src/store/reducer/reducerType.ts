import { IUserData } from "../../helper/localStorage/localStorageService";

export interface IAuthReducer extends IUserData {
  loading: boolean;
  error: {
    data: string;
    success: boolean;
    message: string;
  } | null;
}
