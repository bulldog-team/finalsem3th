import { AxiosResponse } from "axios";
import { IUserData } from "../../localStorage/localStorageService";
import axiosClient from "../axiosClient";

interface IAuthApi {
  login: (
    username: string,
    password: string
  ) => Promise<AxiosResponse<IUserData>>;
}

// Authenticate axios
const authApi: IAuthApi = {
  login: async (
    username: string,
    password: string
  ): Promise<AxiosResponse<IUserData>> => {
    const url: string = `${process.env.REACT_APP_API_URL}/auth/login`;
    const data: object = { username, password };
    return axiosClient.post(url, data);
  },
};

export default authApi;
