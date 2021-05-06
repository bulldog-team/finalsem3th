import { AxiosResponse } from "axios";

import axiosClient from "../axiosClient";
import { ICreatePackageForm } from "../../../Component/PackageList/CreatePackage";
import { IEditForm } from "../../../Component/PackageList/EditPackage";

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
  type: string;
  packageStatus: string;
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

  userUpdatePackageInfo: (
    packageId: number,
    request: IEditForm
  ) => Promise<AxiosResponse<string>>;

  userUpdateCashPayment: (packageId: number) => Promise<AxiosResponse<string>>;

  createPackage: (
    data: ICreatePackageForm
  ) => Promise<AxiosResponse<ICreatePackageFormRepsone>>;

  adminDeletePackage: (packageId: number) => Promise<AxiosResponse<string>>;
}

// Package api
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

  userUpdatePackageInfo: (packageId, request) => {
    const url = `${process.env.REACT_APP_API_URL}/package/info/${packageId}`;
    return axiosClient.patch(url, request);
  },
  userUpdateCashPayment: (packageId) => {
    const url = `${process.env.REACT_APP_API_URL}/package/payment/${packageId}`;
    return axiosClient.put(url);
  },

  createPackage: (data) => {
    const url = `${process.env.REACT_APP_API_URL}/package/init`;
    return axiosClient.post(url, data);
  },

  adminDeletePackage: (packageId) => {
    const url = `${process.env.REACT_APP_API_URL}/package/info/${packageId}`;
    return axiosClient.delete(url);
  },
};

export default packageApi;
