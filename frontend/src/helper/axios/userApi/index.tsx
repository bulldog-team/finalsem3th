import { AxiosResponse } from "axios";
import { UserInfoForm } from "../../../Component/UserInfo/UserInfo";
import { localStorageService } from "../../localStorage/localStorageService";
import axiosClient from "../axiosClient";

export type UserInfoType = {
  username: string;
  email: string;
  gender: number;
  address: string;
  dob: any;
  phone: string;
  branchId: number;
  imgName: string;
  imgSrc: string;
  imgFile: string | Blob | null;
  isAdminAccept: boolean;
};

export type UserListType = {
  username: string;
  email: string;
  branch: string;
  isAdminAccept: boolean;
  userId: number;
};

interface IUserApi {
  userUpdateUserInfo: (form: FormData) => Promise<AxiosResponse<UserInfoForm>>;
  userGetUserInfo: () => Promise<AxiosResponse<UserInfoType>>;
  getUserList: () => Promise<AxiosResponse<UserListType[]>>;
  adminGetUserInfo: (
    userId: number | undefined
  ) => Promise<AxiosResponse<UserInfoType>>;
}

const UserApi: IUserApi = {
  userUpdateUserInfo: async (
    form: FormData
  ): Promise<AxiosResponse<UserInfoType>> => {
    const userId = localStorageService.getUserId();
    const url: string = `${process.env.REACT_APP_API_URL}/user/info/${userId}`;
    return axiosClient.patch(url, form);
  },
  userGetUserInfo: async () => {
    const userId = localStorageService.getUserId();
    const url = `${process.env.REACT_APP_API_URL}/user/info/${userId}`;
    return axiosClient.get(url);
  },
  getUserList: async () => {
    const url = `${process.env.REACT_APP_API_URL}/user/`;
    return axiosClient.get(url);
  },

  adminGetUserInfo: async (userId) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/info/${userId}`;
    return axiosClient.get(url);
  },
};

export default UserApi;
