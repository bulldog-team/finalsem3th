import Heading from "../Heading/Heading";
import * as yup from "yup";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import CustomField from "../Field/Field";
import { Col, Row, Form, DatePicker, Button } from "antd";

type UserInfoForm = {
  username: string;
  email: string;
  gender: boolean;
  age: number;
  address: string;
  dob: any;
  phone: string;
  branchId: number;
};

const UserInfo = () => {
  const initForm: UserInfoForm = {
    username: "",
    email: "",
    gender: true,
    age: 1,
    address: "",
    branchId: 1,
    dob: "",
    phone: "",
  };

  const UserInfoSchema: yup.SchemaOf<UserInfoForm> = yup.object({
    address: yup.string().defined(),
    age: yup.number().integer().defined(),
    branchId: yup.number().integer().defined(),
    dob: yup.string().defined(),
    email: yup.string().email().defined(),
    gender: yup.boolean().defined(),
    phone: yup.string().defined(),
    username: yup.string().defined(),
  });

  const layout = {
    labelCol: {
      span: 6,
      offset: 1,
    },
    wrapperCol: {
      span: 10,
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
    // resolver: yupResolver(UserInfoSchema),
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
                  name="address"
                  label="Address"
                  control={control}
                  errors={errors}
                  type="text"
                />
                <CustomField
                  name="phone"
                  label="Phone"
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
                <CustomField
                  name="gender"
                  label="Gender"
                  control={control}
                  errors={errors}
                  type="select"
                  options={[
                    { value: "true", name: "Male" },
                    { value: "false", name: "Female" },
                  ]}
                />
                <CustomField
                  name="branch"
                  label="Branch"
                  control={control}
                  errors={errors}
                  type="select"
                  options={[
                    { value: "1", name: "1" },
                    { value: "2", name: "2" },
                  ]}
                />
                <CustomField
                  name="dob"
                  label="Date of Birth"
                  control={control}
                  errors={errors}
                  type="datePicker"
                />
              </Col>
            </Row>
            <Row justify="center" style={{ margin: "2rem 0" }}>
              <Button type="primary" htmlType="submit">
                Send
              </Button>
            </Row>
          </Form>
        </div>
      </div>
    </>
  );
};

export default UserInfo;
