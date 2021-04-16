import { AxiosResponse } from "axios";
import { UserInfoForm } from "../../../Component/UserInfo/UserInfo";
import { IcreateUserForm } from "../../../Component/UserList/CreateUserModal";
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
  branchName: string;
};

export type UserListType = {
  username: string;
  email: string;
  branch: string;
  isAdminAccept: boolean;
  userId: number;
};

export type AdminCreateUserReponse = {
  username: string;
  email: string;
  userId: number;
};

export type AdminDeleteuserRepsonse = {
  username: string;
  userId: number;
};

export type UpdatePasswordResponseType = {
  username: string;
  userId: number;
};

export type UpdatePasswordRequestType = {
  password: string;
  confirmPassword: string;
  currentPassword: string;
};

interface IUserApi {
  userUpdateUserInfo: (form: FormData) => Promise<AxiosResponse<UserInfoForm>>;

  userGetUserInfo: () => Promise<AxiosResponse<UserInfoType>>;

  getUserList: () => Promise<AxiosResponse<UserListType[]>>;

  adminGetUserInfo: (
    userId: number | undefined
  ) => Promise<AxiosResponse<UserInfoType>>;

  adminUpdateUserInfo: (
    userId: number | undefined
  ) => Promise<AxiosResponse<UserInfoType>>;

  adminCreateUser: (
    form: IcreateUserForm
  ) => Promise<AxiosResponse<AdminCreateUserReponse>>;

  adminDeleteUser: (
    userId: number
  ) => Promise<AxiosResponse<AdminDeleteuserRepsonse>>;

  updatePassword: (
    data: UpdatePasswordRequestType
  ) => Promise<AxiosResponse<UpdatePasswordResponseType>>;
}

const UserApi: IUserApi = {
  userUpdateUserInfo: async (form) => {
    const userId = localStorageService.getUserId();
    const url: string = `${process.env.REACT_APP_API_URL}/user/info/${userId}`;
    return axiosClient.patch(url, form);
  },

  userGetUserInfo: async () => {
    const userId = localStorageService.getUserId();
    const url = `${process.env.REACT_APP_API_URL}/user/info/${userId}`;
    return axiosClient.get(url);
  },

  updatePassword: async (form: UpdatePasswordRequestType) => {
    const userId = localStorageService.getUserId();
    const url = `${process.env.REACT_APP_API_URL}/user/info/${userId}`;
    return axiosClient.put(url, form);
  },

  getUserList: async () => {
    const url = `${process.env.REACT_APP_API_URL}/user/`;
    return axiosClient.get(url);
  },

  adminGetUserInfo: async (userId) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/info/${userId}`;
    return axiosClient.get(url);
  },

  adminUpdateUserInfo: async (userId) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/info/${userId}`;
    return axiosClient.patch(url);
  },

  adminCreateUser: async (form) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/info`;
    return axiosClient.post(url, form);
  },

  adminDeleteUser: async (userId) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/info/${userId}`;
    return axiosClient.delete(url);
  },
};

export default UserApi;
