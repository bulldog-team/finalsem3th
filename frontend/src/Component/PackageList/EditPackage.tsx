import { yupResolver } from "@hookform/resolvers/yup";
import moment from "moment";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import * as yup from "yup";
import { Button, Col, Form, Modal, Row } from "antd";

import packageApi from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";

interface IProps {
  isEditModal: boolean;
  setIsEditModal: Dispatch<SetStateAction<boolean | undefined>>;
  setUpdate: Dispatch<SetStateAction<boolean>>;
  packageId: number;
}

export interface IEditForm {
  senderName: string;
  senderAddress: string;
  receiveName: string;
  receiveAddress: string;
  deliveryType: string;
  dateSent: string;
  pincode: number;
  weight: number;
}

type DeliveryTypeOptions = {
  name: string;
  value: string;
  price: number;
};

const EditPackageModal: FC<IProps> = (props) => {
  const { isEditModal, setIsEditModal, setUpdate, packageId } = props;

  const EditFormSchema: yup.SchemaOf<IEditForm> = yup.object({
    senderName: yup.string().required("This field is required!"),
    senderAddress: yup.string().required("This field is required!"),
    receiveName: yup.string().required("This field is required!"),
    receiveAddress: yup.string().required("This field is required!"),
    dateSent: yup.string().required("This field is required!"),
    deliveryType: yup.string().required("This field is required!"),
    pincode: yup.number().required("This field is required!"),
    weight: yup.number().required("This field is required!"),
  });

  const [deliveryType, setDeliveryType] = useState<DeliveryTypeOptions[]>([]);

  const {
    control,
    formState: { errors },
    reset,
    handleSubmit,
  } = useForm<IEditForm>({
    resolver: yupResolver(EditFormSchema),
  });

  useEffect(() => {
    const fetchDeliveryType = async () => {
      const response = await packageApi.getPriceList();
      const transfer = response.data.map((item) => ({
        name: item.typeName,
        value: item.typeName,
        price: item.unitPrice,
      }));
      setDeliveryType(transfer);
    };

    const fetchPackageInfo = async () => {
      const response = await packageApi.userGetPackageInfo(packageId);
      reset({
        dateSent: moment(response.data.dateSent).format("YYYY-MM-DD"),
        deliveryType: response.data.deliveryType,
        pincode: response.data.pincode,
        receiveAddress: response.data.receiveAddress,
        receiveName: response.data.receiveName,
        senderAddress: response.data.senderAddress,
        senderName: response.data.senderName,
        weight: response.data.weight,
      });
    };
    fetchDeliveryType();
    fetchPackageInfo();
  }, [reset, packageId]);

  const layout = {
    labelCol: {
      span: 7,
      offset: 1,
    },
    wrapperCol: {
      span: 15,
    },
  };

  const handleEditPackage: SubmitHandler<IEditForm> = async (data) => {
    await packageApi.userUpdatePackageInfo(packageId, data);
    setUpdate((pre) => !pre);
    setIsEditModal(false);
  };

  const handleClose = () => {
    setIsEditModal(false);
  };

  return (
    <Modal
      title="Edit package"
      centered
      visible={isEditModal}
      onCancel={handleClose}
      footer={[
        <Button key="back" onClick={handleClose}>
          Return
        </Button>,
        <Button key="submit" htmlType="submit" type="primary" form="editForm">
          Update
        </Button>,
      ]}
      width={1100}
    >
      <Form
        onFinish={handleSubmit(handleEditPackage)}
        {...layout}
        id="editForm"
      >
        <Row>
          <Col xs={24} sm={12}>
            <CustomField
              control={control}
              errors={errors}
              name="senderName"
              label="Sender's Name"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="senderAddress"
              label="Sender's Address"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="receiveName"
              label="Receiver's Name"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="receiveAddress"
              label="Receiver's Address"
              type="text"
            />
          </Col>
          <Col xs={24} sm={12}>
            <CustomField
              control={control}
              errors={errors}
              name="pincode"
              label="Pincode"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="weight"
              label="Weight"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="deliveryType"
              label="Delivery Type"
              options={deliveryType}
              type="select"
            />
            <CustomField
              name="dateSent"
              label="Date sent"
              control={control}
              errors={errors}
              type="datePicker"
            />
          </Col>
        </Row>
      </Form>
    </Modal>
  );
};

export default EditPackageModal;
