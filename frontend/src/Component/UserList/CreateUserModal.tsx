import { yupResolver } from "@hookform/resolvers/yup";
import { Button, Form, message, Modal } from "antd";
import { Dispatch, FC, SetStateAction } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import * as yup from "yup";
import UserApi from "../../helper/axios/userApi";
import CustomField from "../Field/Field";

interface CreateUserModalProps {
  isCreateModalOpen: boolean;
  setIsCreateModalOpen: Dispatch<SetStateAction<boolean>>;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

export interface IcreateUserForm {
  username: string;
  password: string;
  confirmPassword: string;
  email: string;
}

const CreateUserModal: FC<CreateUserModalProps> = (props) => {
  const { isCreateModalOpen, setIsCreateModalOpen, setUpdate } = props;

  const CreateUserFormSchema: yup.SchemaOf<IcreateUserForm> = yup.object({
    username: yup.string().required("This field is required"),
    password: yup.string().min(6).required("This field is required"),
    confirmPassword: yup
      .string()
      .min(6)
      .required("This field is required")
      .oneOf(
        [yup.ref("password")],
        "Confirm password must match current password"
      ),
    email: yup.string().email().required("This field is required"),
  });

  const defaultValues: IcreateUserForm = {
    username: "",
    confirmPassword: "",
    email: "",
    password: "",
  };

  const {
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = useForm<IcreateUserForm>({
    defaultValues,
    resolver: yupResolver(CreateUserFormSchema),
  });

  const handleCreateUser: SubmitHandler<IcreateUserForm> = async (
    data: IcreateUserForm
  ) => {
    console.log(typeof data);
    try {
      const response = await UserApi.adminCreateUser(data);
      setIsCreateModalOpen(false);
      setUpdate((pre) => !pre);
      reset();
    } catch (error) {
      message.error(error.response.data, 5);
    }
  };

  const handleCloseModal = () => {
    setIsCreateModalOpen(false);
  };

  return (
    <>
      <Modal
        title="Create User"
        visible={isCreateModalOpen}
        onCancel={handleCloseModal}
        footer={[
          <Button key="back" onClick={handleCloseModal}>
            Return
          </Button>,
          <Button
            key="submit"
            htmlType="submit"
            type="primary"
            form="createUserForm"
          >
            Create
          </Button>,
        ]}
      >
        <Form onFinish={handleSubmit(handleCreateUser)} id="createUserForm">
          <CustomField
            name="username"
            control={control}
            errors={errors}
            placeholder="Username"
            type="text"
          />
          <CustomField
            name="password"
            control={control}
            errors={errors}
            placeholder="Password"
            type="password"
          />
          <CustomField
            name="confirmPassword"
            control={control}
            errors={errors}
            placeholder="Confirm Password"
            type="password"
          />
          <CustomField
            name="email"
            control={control}
            errors={errors}
            placeholder="Email"
            type="text"
          />
        </Form>
      </Modal>
    </>
  );
};

export default CreateUserModal;
