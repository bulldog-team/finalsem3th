import { Button, Form } from "antd";
import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import packageApi, { DeliveryType } from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";
import { IcreateUserForm } from "../UserList/CreateUserModal";

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
  }, []);

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
      width={960}
    >
      <Form onFinish={handleSubmit(handleCreatePackage)} id="createPackage">
        <CustomField
          control={control}
          errors={errors}
          name="deliveryType"
          label="Delivery Type"
          options={deliveryType}
          type="select"
        />
      </Form>
    </Modal>
  );
};

export default CreatePackage;
