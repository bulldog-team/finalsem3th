import { Col, Descriptions, Row, Select } from "antd";
import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";

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
      centered
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
          {packageInfo?.isPaid ? "Paid" : "Not yet"}
        </Descriptions.Item>
        <Descriptions.Item label="Update Package Status">
          <Select
            value={newPackageStatus}
            style={{ width: 120 }}
            disabled={!packageInfo?.isPaid}
          >
            <Option value="Picked up">Picked up</Option>
            <Option value="Sending">Sending</Option>
            <Option value="Received">Received</Option>
          </Select>
        </Descriptions.Item>
        <Descriptions.Item label="">
          {!packageInfo?.isPaid && (
            <PayPalScriptProvider options={{ "client-id": "test" }}>
              <PayPalButtons
                forceReRender={[packageInfo?.totalPrice]}
                createOrder={(data, actions) => {
                  if (packageInfo?.totalPrice) {
                    return actions.order.create({
                      purchase_units: [
                        {
                          amount: {
                            value: packageInfo.totalPrice.toString(),
                          },
                          shipping: {
                            address: {
                              address_line_1: packageInfo.receiveAddress,
                              address_line_2: packageInfo.receiveAddress,
                              admin_area_1: packageInfo.receiveAddress,
                              admin_area_2: packageInfo.receiveAddress,
                              country_code: packageInfo.receiveAddress,
                              postal_code: packageInfo.receiveAddress,
                            },
                            name: {
                              full_name: packageInfo.receiveName,
                            },
                          },
                        },
                      ],
                    });
                  }
                  return new Promise((resolve, reject) => {
                    reject("Failed!");
                  });
                }}
                style={{ layout: "horizontal" }}
              />
            </PayPalScriptProvider>
          )}
        </Descriptions.Item>
      </Descriptions>
    </Modal>
  );
};

export default ViewPackageInfo;
