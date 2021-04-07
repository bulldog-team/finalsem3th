import { Button, Form, Row } from "antd";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { UserOutlined, LockOutlined } from "@ant-design/icons";

import CustomField from "../../Component/Field/Field";
import { Dispatch, FC, SetStateAction } from "react";

type ILoginForm = {
  username: string;
  password: string;
};

interface IloginProps {
  setRedirectToReferrer: Dispatch<SetStateAction<boolean>>;
}

const LoginPage: FC<IloginProps> = () => {
  const LoginSchema = yup.object().shape({
    username: yup.string().required("Error.required"),
    password: yup
      .string()
      .required("Error.required")
      .min(6, "Error.needMoreCharacter"),
  });

  const defaultValues: ILoginForm = {
    username: "",
    password: "",
  };

  const { handleSubmit, control, errors, reset } = useForm<ILoginForm>({
    defaultValues,
    resolver: yupResolver(LoginSchema),
  });

  const handleLoginForm: SubmitHandler<ILoginForm> = (data: ILoginForm) => {
    console.log(data);
    reset(defaultValues);
  };

  return (
    <div className="login">
      <Form onFinish={handleSubmit(handleLoginForm)}>
        <div className="login__input">
          <CustomField
            name="username"
            control={control}
            errors={errors}
            placeholder="Username"
            type="text"
            prefix={<UserOutlined />}
          />
        </div>
        <div className="login__input">
          <CustomField
            control={control}
            errors={errors}
            name="password"
            placeholder="Password"
            type="password"
            prefix={<LockOutlined />}
          />
        </div>
        <Button type="primary" htmlType="submit" block>
          Login
        </Button>
      </Form>
      <Row className="login__recover">{"Auth.forgotPassword"}</Row>
    </div>
  );
};

export default LoginPage;
