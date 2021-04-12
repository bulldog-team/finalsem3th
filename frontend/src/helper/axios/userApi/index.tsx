import axios, { AxiosResponse } from "axios";
import { IFileState, UserInfoForm } from "../../../Component/UserInfo/UserInfo";
import { localStorageService } from "../../localStorage/localStorageService";
import axiosClient from "../axiosClient";

type UserInfoType = {
  username: string;
  email: string;
  gender: boolean;
  address: string;
  dob: any;
  phone: string;
  branchId: number;
  imgName: string;
  imgSrc: string;
  imgFile: string | Blob | null;
  IsAdminAccept: boolean;
};

interface IUserApi {
  userUpdateUserInfo: (form: FormData) => Promise<AxiosResponse<UserInfoForm>>;
  userGetUserInfo: () => Promise<AxiosResponse<UserInfoType>>;
}

const UserApi: IUserApi = {
  userUpdateUserInfo: async (
    form: FormData
  ): Promise<AxiosResponse<UserInfoType>> => {
    const userId = localStorageService.getUserId();
    const url: string = `${process.env.REACT_APP_API_URL}/user/${userId}`;
    return axiosClient.patch(url, form);
  },
  userGetUserInfo: async () => {
    const userId = localStorageService.getUserId();
    const url = `${process.env.REACT_APP_API_URL}/user/${userId}`;
    return axiosClient.get(url);
  },
};

export default UserApi;
