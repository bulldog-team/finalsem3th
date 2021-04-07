import { yupResolver } from "@hookform/resolvers/yup";
import { Form, Button } from "antd";
import { SubmitHandler, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import * as yup from "yup";
import { UserOutlined, LockOutlined, MailOutlined } from "@ant-design/icons";

import CustomField from "../../Component/Field/Field";
import VerifyPage from "../Verify/Verify";

type IRegisterForm = {
  username: string;
  password: string;
  confirmPassword: string;
  email: string;
};

const RegisterPage = () => {
  const { t } = useTranslation();

  const defaultValues: IRegisterForm = {
    username: "",
    confirmPassword: "",
    email: "",
    password: "",
  };

  const RegisterSchema = yup.object().shape({
    username: yup.string().required(t("Error.required")),
    password: yup
      .string()
      .required(t("Error.required"))
      .min(6, t("Error.needMoreCharacter")),
    confirmPassword: yup
      .string()
      .required(t("Error.required"))
      .oneOf([yup.ref("password")], t("Error.mustMatch"))
      .min(6, t("Error.needMoreCharacter")),
    email: yup
      .string()
      .email(t("Error.validEmail"))
      .required(t("Error.required")),
  });

  const { handleSubmit, control, errors, reset } = useForm<IRegisterForm>({
    defaultValues,
    resolver: yupResolver(RegisterSchema),
  });

  const handleRegisterForm: SubmitHandler<IRegisterForm> = (
    data: IRegisterForm
  ) => {
    console.log(data);
    reset(defaultValues);
  };

  return (
    <div className="register">
      <Form onFinish={handleSubmit(handleRegisterForm)}>
        <CustomField
          name="username"
          control={control}
          errors={errors}
          placeholder="Username"
          type="text"
          prefix={<UserOutlined />}
        />
        <CustomField
          name="password"
          control={control}
          errors={errors}
          placeholder={t("RegisterPage.passwordPlaceholder")}
          type="password"
          prefix={<LockOutlined />}
        />
        <CustomField
          name="confirmPassword"
          control={control}
          errors={errors}
          placeholder="Confirm Password"
          type="password"
          prefix={<LockOutlined />}
        />
        <CustomField
          name="email"
          control={control}
          errors={errors}
          placeholder="Email"
          type="text"
          prefix={<MailOutlined />}
        />
        <Button type="primary" htmlType="submit" block>
          Register
        </Button>
      </Form>
    </div>
  );
};

export default RegisterPage;
