import { Descriptions } from "antd";
import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { PayPalButton } from "react-paypal-button-v2";

import packageApi, { PackageInfo } from "../../helper/axios/packageApi";

interface ViewpackageInfoProps {
  isViewModalOpen: boolean;
  setIsViewModalOpen: Dispatch<SetStateAction<boolean>>;
  packageId: number;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

const ViewPackageInfo: FC<ViewpackageInfoProps> = (props) => {
  const { isViewModalOpen, setIsViewModalOpen, packageId, setUpdate } = props;

  const [packageInfo, setPackageInfo] = useState<PackageInfo>();

  const handleOk = async () => {};

  const handleCancel = () => {
    setIsViewModalOpen(false);
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await packageApi.userGetPackageInfo(packageId);
      setPackageInfo(response.data);
      console.log(response);
    };
    fetchData();
  }, [packageId]);
  return (
    <Modal
      title="Package Info"
      visible={isViewModalOpen}
      onOk={handleOk}
      onCancel={handleCancel}
      width={960}
    >
      <Descriptions bordered>
        <Descriptions.Item label="Created by">
          {packageInfo?.createdBy}
        </Descriptions.Item>
        <Descriptions.Item label="Sender's name">
          {packageInfo?.senderName}
        </Descriptions.Item>
        <Descriptions.Item label="Sender's address">
          {packageInfo?.senderAddress}
        </Descriptions.Item>
        <Descriptions.Item label="Receiver's name">
          {packageInfo?.receiveName}
        </Descriptions.Item>
        <Descriptions.Item label="Receiver's address">
          {packageInfo?.receiveAddress}
        </Descriptions.Item>
        <Descriptions.Item label="Package status">
          {packageInfo?.status}
        </Descriptions.Item>
        <Descriptions.Item label="Pin code">
          {packageInfo?.pincode}
        </Descriptions.Item>
        <Descriptions.Item label="Deliver type">
          {packageInfo?.deliveryType}
        </Descriptions.Item>
        <Descriptions.Item label="Price">
          {packageInfo?.totalPrice}
        </Descriptions.Item>
        <Descriptions.Item label="Payment Status">
          {packageInfo?.totalPrice ? "Paid" : "Not yet"}
        </Descriptions.Item>
        <Descriptions.Item label="">
          <PayPalButton
            amount={packageInfo?.totalPrice}
            // shippingPreference="NO_SHIPPING" // default is "GET_FROM_FILE"
            options={{
              clientId:
                "ASgR34Pm1N4ifEQLSkP_CCFGaEHfdN_76gpASMKwFi1dPgXBfeS6FkVTVzpsU6dcGQlwO0qoG_g-tZJG",
            }}
            onSuccess={(
              details: { payer: { name: { given_name: string } } },
              data: { orderID: any }
            ) => {
              alert(
                "Transaction completed by " + details.payer.name.given_name
              );
              console.log(details);

              // OPTIONAL: Call your server to save the transaction
              // return fetch("/paypal-transaction-complete", {
              //   method: "post",
              //   body: JSON.stringify({
              //     orderID: data.orderID,
              //   }),
              // });
            }}
          />
        </Descriptions.Item>
      </Descriptions>
    </Modal>
  );
};

export default ViewPackageInfo;
