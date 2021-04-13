import { IUserData } from "../../helper/localStorage/localStorageService";

export interface IAuthReducer extends IUserData {
  loading: boolean;
  error: string | null;
}
