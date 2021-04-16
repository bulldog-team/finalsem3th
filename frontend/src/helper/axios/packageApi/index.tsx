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

interface IPackageApi {
  getPriceList: () => Promise<AxiosResponse<DeliveryType[]>>;
  updatePriceList: (
    data: DeliveryTypeUpdate[]
  ) => Promise<AxiosResponse<DeliveryType[]>>;
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
};

export default packageApi;
