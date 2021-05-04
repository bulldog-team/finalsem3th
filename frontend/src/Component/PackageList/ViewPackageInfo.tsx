import { Col, Descriptions, Row, Select } from "antd";
import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import { PayPalButton } from "react-paypal-button-v2";

import packageApi, {
  PackageInfo,
  UserUpdateStatusType,
} from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";
import { resolve } from "node:path";
import DeliveryStep from "../Step/DeliveryStep";

interface ViewpackageInfoProps {
  isViewModalOpen: boolean;
  setIsViewModalOpen: Dispatch<SetStateAction<boolean>>;
  packageId: number;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

const ViewPackageInfo: FC<ViewpackageInfoProps> = (props) => {
  const { Option } = Select;
  const [newPackageStatus, setNewPackageStatus] = useState<string>("");
  const [isUpdate, setIsUpdate] = useState<boolean>(false);

  const { isViewModalOpen, setIsViewModalOpen, packageId, setUpdate } = props;

  const [packageInfo, setPackageInfo] = useState<PackageInfo>();

  const handleOk = async () => {
    console.log(newPackageStatus);
    console.log(packageId);
    const data: UserUpdateStatusType = {
      txtStatus: newPackageStatus,
    };
    const response = await packageApi.userUpdatePackageStatus(packageId, data);
    setIsViewModalOpen(false);
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
      if (
        packageInfo?.deliveryType === "VPP" ||
        (packageInfo?.deliveryType !== "VPP" && packageInfo?.isPaid === true)
      ) {
        setIsUpdate(true);
      }
      console.log(response);
    };
    fetchData();
  }, [
    packageId,
    packageInfo?.status,
    packageInfo?.isPaid,
    packageInfo?.deliveryType,
  ]);

  return packageInfo ? (
    <Modal
      centered
      title="Package Info"
      visible={isViewModalOpen}
      onOk={handleOk}
      onCancel={handleCancel}
      width={960}
      destroyOnClose
    >
      <DeliveryStep current={packageInfo.status} />
      <Descriptions column={2} bordered>
        <Descriptions.Item label="Created by">
          {packageInfo?.createdBy}
        </Descriptions.Item>
        <Descriptions.Item label="Deliver type">
          {packageInfo?.deliveryType}
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
            disabled={!isUpdate}
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
              shippingPreference="NO_SHIPPING"
              // shippingPreference="NO_SHIPPING" // default is "GET_FROM_FILE"
              onSuccess={(details: any, data: any) => {
                console.log("DETAILS", details);
                console.log("DATA", data);

                // OPTIONAL: Call your server to save the transaction
                return packageApi
                  .userUpdatePayment(packageInfo.packageId)
                  .then(() => {
                    setUpdate((pre) => !pre);
                    console.log("Check");
                    setPackageInfo((pre) => {
                      if (pre) {
                        return {
                          ...pre,
                          isPaid: true,
                        };
                      }
                    });
                  });
              }}
            />
          )}
        </Descriptions.Item>
      </Descriptions>
    </Modal>
  ) : (
    <></>
  );
};

export default ViewPackageInfo;
