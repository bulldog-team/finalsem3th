import { AxiosResponse } from "axios";

import axiosClient from "../axiosClient";
import { ICreatePackageForm } from "../../../Component/PackageList/CreatePackage";

export type DeliveryType = {
  typeId: number;
  typeName: string;
  unitPrice: number;
};

export type DeliveryTypeUpdate = {
  typeName: string;
  unitPrice: number;
};

export type PackageListType = {
  packageId: number;
  senderName: string;
  receiveName: string;
  dateSent: Date;
  totalPrice: number;
  isPaid: boolean;
};

export type PackageInfo = {
  packageId: number;
  senderName: string;
  senderAddress: string;
  receiveName: string;
  receiveAddress: string;
  createdBy: string;
  deliveryType: string;
  dateSent: string;
  dateReceived: string;
  totalPrice: number;
  distance: number;
  pincode: number;
  weight: number;
  status: string;
  isPaid: boolean;
};

export type ICreatePackageFormRepsone = ICreatePackageForm & {
  distance: number;
  totalPrice: number;
};

export type UserUpdateStatusType = {
  txtStatus: string;
};

interface IPackageApi {
  getPriceList: () => Promise<AxiosResponse<DeliveryType[]>>;
  updatePriceList: (
    data: DeliveryTypeUpdate[]
  ) => Promise<AxiosResponse<DeliveryType[]>>;

  getPackageList: () => Promise<AxiosResponse<PackageListType[]>>;

  userGetPackageInfo: (
    packageId: number
  ) => Promise<AxiosResponse<PackageInfo>>;

  userUpdatePackageStatus: (
    packageId: number,
    data: UserUpdateStatusType
  ) => Promise<AxiosResponse<object>>;

  userUpdatePayment: (packageId: number) => Promise<AxiosResponse<string>>;

  createPackage: (
    data: ICreatePackageForm
  ) => Promise<AxiosResponse<ICreatePackageFormRepsone>>;
}

const packageApi: IPackageApi = {
  getPriceList: () => {
    const url = `${process.env.REACT_APP_API_URL}/admin/type`;
    return axiosClient.get(url);
  },

  updatePriceList: (data) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/type`;
    return axiosClient.patch(url, data);
  },

  getPackageList: () => {
    const url = `${process.env.REACT_APP_API_URL}/package/init`;
    return axiosClient.get(url);
  },

  userGetPackageInfo: (packageId) => {
    const url = `${process.env.REACT_APP_API_URL}/package/info/${packageId}`;
    return axiosClient.get(url);
  },

  userUpdatePackageStatus: (packageId: number, data: UserUpdateStatusType) => {
    const url = `${process.env.REACT_APP_API_URL}/package/info/${packageId}`;
    console.log(url);

    return axiosClient.post(url, data);
  },

  userUpdatePayment: (packageId: number) => {
    const url = `${process.env.REACT_APP_API_URL}/package/info/${packageId}`;
    return axiosClient.put(url);
  },

  createPackage: (data) => {
    const url = `${process.env.REACT_APP_API_URL}/package/init`;
    return axiosClient.post(url, data);
  },
};

export default packageApi;
