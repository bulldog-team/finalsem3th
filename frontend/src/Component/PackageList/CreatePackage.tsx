import { Button, Col, Form, Row } from "antd";
import Modal from "antd/lib/modal/Modal";
import moment from "moment";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import packageApi, { DeliveryType } from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";

interface NewPackageProps {
  isCreateModalOpen: boolean;
  setIsCreateModalOpen: Dispatch<SetStateAction<boolean>>;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

interface ICreatePackageForm {
  deliveryType: string;
}

type DeliveryTypeOptions = {
  name: string;
  value: string;
  price: number;
};

const CreatePackage: FC<NewPackageProps> = (props) => {
  const { isCreateModalOpen, setIsCreateModalOpen, setUpdate } = props;
  const [deliveryType, setDeliveryType] = useState<DeliveryTypeOptions[]>([]);
  const {
    control,
    formState: { errors },
    reset,
    handleSubmit,
  } = useForm<ICreatePackageForm>();

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
      reset({ deliveryType: "Speed Post" });
      console.log(response);
    };
    fetchDeliveryType();
  }, [reset]);

  const handleCreatePackage: SubmitHandler<ICreatePackageForm> = async (
    data
  ) => {
    console.log(data);
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
              name="sendername"
              label="Sender's Name"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="senderaddress"
              label="Sender's Address"
              type="text"
            />
            <CustomField
              control={control}
              errors={errors}
              name="receiveName"
              label="Receiver"
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
              label="Date of Birth"
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
