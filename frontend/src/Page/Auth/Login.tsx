import { Button, Form } from "antd";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { useDispatch } from "react-redux";
import { Dispatch, FC, SetStateAction } from "react";

import CustomField from "../../Component/Field/Field";
import * as actionCreator from "../../store/action/index";

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
    dispatch(actionCreator.handleLogin(data.username, data.username));
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
        <Button
          type="primary"
          htmlType="submit"
          block
          style={{ marginTop: "1rem" }}
        >
          Login
        </Button>
      </Form>
    </div>
  );
};

export default LoginPage;
