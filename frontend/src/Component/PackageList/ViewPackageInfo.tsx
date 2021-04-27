import { Col, Descriptions, Row, Select } from "antd";
import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import { PayPalButton } from "react-paypal-button-v2";

import packageApi, { PackageInfo } from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";
import { resolve } from "node:path";

interface ViewpackageInfoProps {
  isViewModalOpen: boolean;
  setIsViewModalOpen: Dispatch<SetStateAction<boolean>>;
  packageId: number;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

const ViewPackageInfo: FC<ViewpackageInfoProps> = (props) => {
  const { Option } = Select;
  const [newPackageStatus, setNewPackageStatus] = useState<string>("");

  const { isViewModalOpen, setIsViewModalOpen, packageId, setUpdate } = props;

  const [packageInfo, setPackageInfo] = useState<PackageInfo>();

  const handleOk = async () => {
    const response = await packageApi.userUpdatePackageStatus();
    console.log(response);
  };

  const handleCancel = () => {
    setIsViewModalOpen(false);
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await packageApi.userGetPackageInfo(packageId);
      setPackageInfo(response.data);
      if (packageInfo?.status) {
        setNewPackageStatus(packageInfo.status);
      }
      console.log(response);
    };
    fetchData();
  }, [packageId, packageInfo?.status]);
  return (
    <Modal
      centered
      title="Package Info"
      visible={isViewModalOpen}
      onOk={handleOk}
      onCancel={handleCancel}
      width={960}
      destroyOnClose
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
          {packageInfo?.isPaid ? "Paid" : "Not yet"}
        </Descriptions.Item>
        <Descriptions.Item label="Update Package Status">
          <Select
            value={newPackageStatus}
            style={{ width: 120 }}
            disabled={!packageInfo?.isPaid}
            onChange={(value) => {
              setNewPackageStatus(value);
            }}
          >
            <Option value="Picked up">Picked up</Option>
            <Option value="Sending">Sending</Option>
            <Option value="Received">Received</Option>
          </Select>
        </Descriptions.Item>
        <Descriptions.Item label="">
          {packageInfo && !packageInfo.isPaid && (
            <PayPalButton
              amount={packageInfo.totalPrice}
              // shippingPreference="NO_SHIPPING" // default is "GET_FROM_FILE"
              onSuccess={(details: any, data: any) => {
                console.log("DETAILS", details);
                console.log("DATA", data);

                // OPTIONAL: Call your server to save the transaction
                // return fetch("/paypal-transaction-complete", {
                //   method: "post",
                //   body: JSON.stringify({
                //     orderID: data.orderID,
                //   }),
                // });
              }}
            />
          )}
        </Descriptions.Item>
      </Descriptions>
    </Modal>
  );
};

export default ViewPackageInfo;
