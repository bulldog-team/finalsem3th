import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import {
  IUserData,
  localStorageService,
} from "../localStorage/localStorageService";

const axiosClient = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

// handle auto re-login
const getRefreshToken = async (userData: IUserData): Promise<string | null> => {
  return null;
};

// handle axios interceptors
axiosClient.interceptors.request.use(
  async (config: AxiosRequestConfig): Promise<AxiosRequestConfig> => {
    const acToken: string | null = localStorageService.getToken();
    if (acToken) {
      config.headers["Authorization"] = `Bearer ${acToken}`;
    }
    return config;
  },
  (err: AxiosResponse) => Promise.reject(err)
);

axiosClient.interceptors.response.use(
  (response: AxiosResponse) => {
    if (response && response.data) {
      return response.data;
    }
  },
  async (err: AxiosError): Promise<any> => {
    const originalRequest = { ...err.config, _retry: false };
    if (err.response) {
      if (err.response.status === 403 && !originalRequest._retry) {
        originalRequest._retry = true;
        const userData: IUserData = localStorageService.getUserData();

        const newAcToken: string | null = await getRefreshToken(userData);
        const curUser: IUserData = localStorageService.getUserData();

        if (newAcToken && curUser) {
          axios.defaults.headers.common[
            "Authorization"
          ] = `Bearer ${newAcToken}`;

          const newUser: IUserData = { ...curUser, token: newAcToken };
          localStorageService.setUserData(newUser);
          return axiosClient(originalRequest);
        }
      }
    }
    return Promise.reject(err);
  }
);

export default axiosClient;
