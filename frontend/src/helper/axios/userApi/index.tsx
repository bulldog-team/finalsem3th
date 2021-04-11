import { AxiosResponse } from "axios";
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
  updateUserInfo: (form: FormData) => Promise<AxiosResponse<UserInfoForm>>;
}

const UserApi: IUserApi = {
  updateUserInfo: async (
    form: FormData
  ): Promise<AxiosResponse<UserInfoType>> => {
    const userId = localStorageService.getUserId();
    const url: string = `${process.env.REACT_APP_API_URL}/user/${userId}`;
    return axiosClient.patch(url, form);
  },
};

export default UserApi;
