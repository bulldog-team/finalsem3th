import { Form, Input } from "antd";
import { FC } from "react";
import {
  Control,
  Controller,
  ControllerFieldState,
  ControllerRenderProps,
  FieldErrors,
} from "react-hook-form";

export declare const InputTypes: ["text", "password"];
export declare type InputType = typeof InputTypes[number];

interface IField {
  label?: string;
  name: string;
  placeholder?: string;
  errors: FieldErrors;
  control: Control<any>;
  hasFeedback?: boolean;
  type: InputType;
  prefix?: React.ReactElement;
  className?: string;
}

const CustomField: FC<IField> = (props: IField) => {
  const { label, name, errors, control, type, hasFeedback, ...rest } = props;

  const InputStyle = (
    type: string,
    props: {
      field: ControllerRenderProps;
      fieldState: ControllerFieldState;
    }
  ): React.ReactElement => {
    const { field, fieldState } = props;
    switch (type) {
      case "text":
        return (
          <Input
            {...field}
            {...fieldState}
            {...rest}
            autoComplete={field.name}
          />
        );
      case "password":
        return (
          <Input.Password
            {...field}
            {...fieldState}
            {...rest}
            autoComplete="current-password"
          />
        );
    }
    return <></>;
  };

  return (
    <Form.Item
      label={label}
      validateStatus={errors && errors[name] ? "error" : ""}
      help={errors[name]?.message}
      hasFeedback={hasFeedback}
    >
      <Controller
        control={control}
        name={name}
        render={(props) => InputStyle(type, props)}
      />
    </Form.Item>
  );
};

export default CustomField;
