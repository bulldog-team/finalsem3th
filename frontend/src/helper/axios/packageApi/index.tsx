import { AxiosResponse } from "axios";
import axiosClient from "../axiosClient";

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

interface IPackageApi {
  getPriceList: () => Promise<AxiosResponse<DeliveryType[]>>;
  updatePriceList: (
    data: DeliveryTypeUpdate[]
  ) => Promise<AxiosResponse<DeliveryType[]>>;

  getPackageList: () => Promise<AxiosResponse<PackageListType[]>>;
}

const packageApi: IPackageApi = {
  getPriceList: async () => {
    const url = `${process.env.REACT_APP_API_URL}/admin/type`;
    return axiosClient.get(url);
  },

  updatePriceList: async (data) => {
    const url = `${process.env.REACT_APP_API_URL}/admin/type`;
    return axiosClient.patch(url, data);
  },

  getPackageList: async () => {
    const url = `${process.env.REACT_APP_API_URL}/package/init`;
    return axiosClient.get(url);
  },
};

export default packageApi;
