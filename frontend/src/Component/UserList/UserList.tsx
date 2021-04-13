import { DatePicker, Form } from "antd";
import moment, { Moment } from "moment";
import { useEffect, useState } from "react";
import {
  Controller,
  ControllerRenderProps,
  SubmitHandler,
  useForm,
} from "react-hook-form";

const UserList = () => {
  const [date, setDate] = useState<Moment | null>();
  const dateFormat = "YYYY-MM-DD";
  useEffect(() => {
    reset({
      dob: "2005-05-05",
    });
  }, []);

  const onChange = (date: Moment | null, dateString: string) => {
    console.log(date, dateString);
  };

  interface IForm {
    dob: string;
  }

  const {
    handleSubmit,
    formState: { errors },
    control,
    reset,
    watch,
  } = useForm<IForm>({
    // resolver: yupResolver(UserInfoSchema),
  });

  const dateWatch = watch("dob");

  const handleSubmitForm: SubmitHandler<IForm> = (data: IForm) => {
    console.log(data);
  };

  const onDatePickerChange = (
    date: moment.Moment | null,
    dateString: string,
    props: any
  ) => {
    console.log(dateWatch);
    setDate(date);
    props.field.onChange(dateString);
  };

  return (
    <>
      <Form onFinish={handleSubmit(handleSubmitForm)}>
        <Form.Item label={"label"}>
          <Controller
            control={control}
            name={"dob"}
            render={(props) => (
              <DatePicker
                onChange={(date, dateString) =>
                  onDatePickerChange(date, dateString, props)
                }
                value={moment(props.field.value)}
              />
            )}
          />
        </Form.Item>
      </Form>
    </>
  );
};

export default UserList;
