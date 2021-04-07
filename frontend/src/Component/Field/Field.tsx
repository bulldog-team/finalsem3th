import { Form, Input } from "antd";
import { FC } from "react";
import {
  Control,
  Controller,
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
  control: Control;
  hasFeedback?: boolean;
  type: InputType;
  prefix?: React.ReactElement;
  className?: string;
}

const CustomField: FC<IField> = (props: IField) => {
  const { label, name, errors, control, type, hasFeedback, ...rest } = props;

  const InputStyle = (
    type: string,
    props: ControllerRenderProps
  ): React.ReactElement => {
    const { onChange, onBlur, value, name, ...restProps } = props;
    switch (type) {
      case "text":
        return (
          <Input
            name={name}
            onChange={onChange}
            onBlur={onBlur}
            value={value}
            autoComplete={name}
            {...rest}
            {...restProps}
          />
        );
      case "password":
        return (
          <Input.Password
            name={name}
            onChange={onChange}
            onBlur={onBlur}
            value={value}
            autoComplete="current-password"
            {...rest}
            {...restProps}
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
        render={(props: ControllerRenderProps) => InputStyle(type, props)}
      />
    </Form.Item>
  );
};

export default CustomField;
