import { AxiosResponse } from "axios";
import axiosClient from "../axiosClient";

export type BranchInfoType = {
  branchId: number;
  branchName: string;
  address: string;
};

interface IBranchApi {
  getBranchData: () => Promise<AxiosResponse<BranchInfoType[]>>;
}

const BranchApi: IBranchApi = {
  getBranchData: async () => {
    const url = `${process.env.REACT_APP_API_URL}/branch/info`;
    return axiosClient.get(url);
  },
};

export default BranchApi;
