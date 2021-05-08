import { Button, Form, message, Row } from "antd";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { useDispatch, useSelector } from "react-redux";
import { Dispatch, FC, SetStateAction } from "react";

import CustomField from "../../Component/Field/Field";
import * as actionCreator from "../../store/action/index";
import { AppState } from "../../store/store";
import { Link } from "react-router-dom";

type ILoginForm = {
  username: string;
  password: string;
};

interface IloginProps {
  setRedirectToReferrer: Dispatch<SetStateAction<boolean>>;
}

// Login Page
const LoginPage: FC<IloginProps> = () => {
  const LoginSchema: yup.SchemaOf<ILoginForm> = yup.object({
    username: yup.string().required("This field is required"),
    password: yup.string().required("This field is required"),
  });

  const defaultValues: ILoginForm = {
    username: "",
    password: "",
  };

  const dispatch = useDispatch();

  const {
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = useForm<ILoginForm>({
    defaultValues,
    resolver: yupResolver(LoginSchema),
  });

  const handleLoginForm: SubmitHandler<ILoginForm> = async (
    data: ILoginForm
  ) => {
    dispatch(actionCreator.handleLogin(data.username, data.password));
    reset(defaultValues);
  };

  const auth = useSelector((state: AppState) => state.auth);
  if (auth.error) {
    message.error(auth.error.message, 5);
  }

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
        <Button
          type="primary"
          htmlType="submit"
          block
          style={{ margin: "3rem 0" }}
        >
          Login
        </Button>
      </Form>
      <Row justify="center">
        <Link to="/search">Switch to Search Page</Link>
      </Row>
    </div>
  );
};

export default LoginPage;
