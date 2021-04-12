import { DatePicker, Form, Input, InputNumber, Select } from "antd";
import moment from "moment";
import { FC } from "react";
import {
  Control,
  Controller,
  ControllerFieldState,
  ControllerRenderProps,
  FieldErrors,
} from "react-hook-form";

export declare const InputTypes: [
  "text",
  "password",
  "datePicker",
  "select",
  "number"
];
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
  options?: { value: string; name: string }[];
  defaultValue?: any;
}

const CustomField: FC<IField> = (propsField: IField) => {
  const {
    label,
    name,
    errors,
    control,
    type,
    hasFeedback,
    ...rest
  } = propsField;

  const onDatePickerChange = (
    date: moment.Moment | null,
    dateString: string,
    field: ControllerRenderProps
  ) => {
    field.onChange(dateString);
  };

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
      case "datePicker":
        return (
          <DatePicker
            onChange={(date, dateString) =>
              onDatePickerChange(date, dateString, field)
            }
            defaultValue={moment(propsField.defaultValue, "yyyy-mm-dd")}
          />
        );
      case "select":
        const { Option } = Select;
        return (
          <Select defaultValue={propsField.defaultValue} {...field}>
            {propsField.options?.map((item) => (
              <Option key={item.name} value={item.value}>
                {item.name}
              </Option>
            ))}
          </Select>
        );
      case "number":
        return (
          <InputNumber
            min={1}
            max={200}
            defaultValue={propsField.defaultValue || 1}
            {...field}
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
