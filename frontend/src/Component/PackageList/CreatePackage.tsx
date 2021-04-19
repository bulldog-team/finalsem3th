import { yupResolver } from "@hookform/resolvers/yup";
import { Button, Col, Form, Row } from "antd";
import Modal from "antd/lib/modal/Modal";
import moment from "moment";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import * as yup from "yup";

import packageApi, { DeliveryType } from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";

interface NewPackageProps {
  isCreateModalOpen: boolean;
  setIsCreateModalOpen: Dispatch<SetStateAction<boolean>>;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

export interface ICreatePackageForm {
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

const CreatePackage: FC<NewPackageProps> = (props) => {
  const { isCreateModalOpen, setIsCreateModalOpen, setUpdate } = props;

  const CreateUserFormSchema: yup.SchemaOf<ICreatePackageForm> = yup.object({
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
  } = useForm<ICreatePackageForm>({
    resolver: yupResolver(CreateUserFormSchema),
  });

  const layout = {
    labelCol: {
      span: 7,
      offset: 1,
    },
    wrapperCol: {
      span: 15,
    },
  };

  useEffect(() => {
    const fetchDeliveryType = async () => {
      const response = await packageApi.getPriceList();
      const transfer = response.data.map((item) => ({
        name: item.typeName,
        value: item.typeName,
        price: item.unitPrice,
      }));
      setDeliveryType(transfer);
      reset({
        deliveryType: "Courier",
        dateSent: moment().format("YYYY-MM-DD"),
      });
      console.log(response);
    };
    fetchDeliveryType();
  }, [reset]);

  const handleCreatePackage: SubmitHandler<ICreatePackageForm> = async (
    data
  ) => {
    const response = await packageApi.createPackage(data);
    console.log(response);
  };

  const handleClose = () => {
    setIsCreateModalOpen(false);
  };

  return (
    <Modal
      title="Create Package"
      visible={isCreateModalOpen}
      onCancel={handleClose}
      footer={[
        <Button key="back" onClick={handleClose}>
          Return
        </Button>,
        <Button
          key="submit"
          htmlType="submit"
          type="primary"
          form="createPackage"
        >
          Create
        </Button>,
      ]}
      width={1100}
    >
      <Form
        onFinish={handleSubmit(handleCreatePackage)}
        {...layout}
        id="createPackage"
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

export default CreatePackage;
