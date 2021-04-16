import { useEffect, useState } from "react";
import packageApi, { DeliveryType } from "../../helper/axios/packageApi";

const PackagePriceList = () => {
  const [listPrice, setListPrice] = useState<DeliveryType[]>([]);

  useEffect(() => {
    const fetchPriceList = async () => {
      const response = await packageApi.getPriceList();
      setListPrice(response.data);
      console.log(response);
    };
    fetchPriceList();
  }, []);

  return <>Package List</>;
};

export default PackagePriceList;
