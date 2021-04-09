import Heading from "../Heading/Heading";
import * as yup from "yup";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import CustomField from "../Field/Field";
import { Col, Row, Form } from "antd";

type UserInfoForm = {
  username: string;
  email: string;
  gender: boolean;
  age: number;
  address: string;
  dob: any;
  image: any;
  phone: string;
  branchId: number;
};

const UserInfo = () => {
  const initForm: UserInfoForm = {
    username: "",
    email: "",
    gender: true,
    age: 0,
    address: "",
    branchId: 1,
    dob: "11/01/1994",
    image: "",
    phone: "",
  };

  const UserInfoSchema: yup.SchemaOf<UserInfoForm> = yup.object({
    address: yup.string().defined(),
    age: yup.number().integer().defined(),
    branchId: yup.number().integer().defined(),
    dob: yup.string().defined(),
    email: yup.string().email().defined(),
    gender: yup.boolean().defined(),
    image: yup.string().defined(),
    phone: yup.string().defined(),
    username: yup.string().defined(),
  });

  const layout = {
    labelCol: {
      span: 6,
      offset: 1,
    },
    wrapperCol: {
      span: 12,
    },
  };

  const handleSubmitForm: SubmitHandler<UserInfoForm> = (
    data: UserInfoForm
  ) => {
    console.log(data);
  };

  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<UserInfoForm>({
    defaultValues: initForm,
    resolver: yupResolver(UserInfoSchema),
  });

  return (
    <>
      <div className="px-1 py-1 table userInfo">
        <Heading title="User Information" />
        <div className="userInfo__body">
          <Form {...layout} onFinish={handleSubmit(handleSubmitForm)}>
            <Row>
              <Col xs={16}>
                <CustomField
                  name="username"
                  label="Username"
                  control={control}
                  errors={errors}
                  type="text"
                />

                <CustomField
                  name="email"
                  label="Email"
                  control={control}
                  errors={errors}
                  type="text"
                />
              </Col>
            </Row>
          </Form>
        </div>
      </div>
    </>
  );
};

export default UserInfo;
