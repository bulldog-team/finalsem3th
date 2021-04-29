import { AxiosResponse } from "axios";
import { ISearchForm } from "../../../Page/Search/Search";
import axiosClient from "../axiosClient";

interface ISearchApi {
  getPackageInfo: (
    data: ISearchForm
  ) => Promise<AxiosResponse<SearchResponse[]>>;
}

export type SearchResponse = {
  packageId: string;
  senderAddress: string;
  receiveAddress: string;
  userId: string;
  packageType: string;
  dateSent: Date;
  dateReceived: Date;
  totalPrice: number;
  distance: number;
  pincode: number;
  weight: number;
  packageStatus: string;
  isPaid: boolean;
};

const searchApi: ISearchApi = {
  getPackageInfo: (data: ISearchForm) => {
    // http://localhost:5000/package/search?PackageId=1&pincode=700000
    const url = `${process.env.REACT_APP_API_URL}/package/search`;
    return axiosClient.get(url, {
      params: { packageId: data.packageId, pincode: data.pincode },
    });
  },
};

export default searchApi;
