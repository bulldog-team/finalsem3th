import Heading from "../Heading/Heading";
import * as yup from "yup";
import { SubmitHandler, useForm, useFormState } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import CustomField from "../Field/Field";
import { UploadOutlined } from "@ant-design/icons";
import { Col, Row, Form, Button, Image, Upload } from "antd";
import { useEffect, useRef, useState } from "react";
import UserApi from "../../helper/axios/userApi";
import moment, { Moment } from "moment";

export type UserInfoForm = {
  username: string;
  email: string;
  gender: boolean;
  address: string;
  dob: any;
  phone: string;
  branchId: number;
};

export interface IFileState {
  imgName: string;
  imgSrc: string;
  imgFile: string | Blob | null;
}

const UserInfo = () => {
  const initForm: UserInfoForm = {
    username: "",
    email: "",
    gender: true,
    address: "",
    branchId: 1,
    dob: "",
    phone: "",
  };

  const UserInfoSchema: yup.SchemaOf<UserInfoForm> = yup.object({
    address: yup.string().defined(),
    age: yup.number().integer().defined(),
    branchId: yup
      .number()
      .integer()
      .defined()
      .required("This field is required"),
    dob: yup.string().defined(),
    email: yup.string().email().defined().required("This field is required"),
    gender: yup.boolean().defined(),
    phone: yup.string().defined(),
    username: yup.string().defined().required("This field is required"),
  });

  const [imgFile, setImgFile] = useState<IFileState>({
    imgName: "",
    imgSrc: "",
    imgFile: "",
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

  const handleSubmitForm: SubmitHandler<UserInfoForm> = async (
    data: UserInfoForm
  ) => {
    const formData = new FormData();
    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value);
    }
    if (imgFile.imgFile) {
      formData.append("imgName", imgFile.imgName);
      formData.append("imgSrc", imgFile.imgSrc);
      formData.append("imgFile", imgFile.imgFile);
    } else {
      formData.append("imgName", "");
      formData.append("imgSrc", "");
      formData.append("imgFile", "");
    }

    try {
      const response = await UserApi.userUpdateUserInfo(formData);
      console.log(response);
    } catch (err) {
      console.log(err);
    }
  };

  const {
    handleSubmit,
    formState: { errors },
    control,
    reset,
    getValues,
  } = useForm<UserInfoForm>({
    defaultValues: initForm,
    // resolver: yupResolver(UserInfoSchema),
  });

  const loadFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    if (e.target.files && e.target.files[0]) {
      let imgFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        if (x.target && typeof x.target.result === "string") {
          setImgFile({
            imgFile,
            imgSrc: x.target.result,
            imgName: imgFile.name,
          });
        } else {
          setImgFile({
            imgFile: null,
            imgSrc: "",
            imgName: "",
          });
        }
      };
      reader.readAsDataURL(imgFile);
    }
  };

  const fileUpload = useRef<HTMLInputElement>(null);

  const showFileUpload = () => {
    if (fileUpload.current) {
      fileUpload.current.click();
    }
  };

  const [date, setDate] = useState<Moment>();
  const dateFormat = "YYYY-MM-DD";

  useEffect(() => {
    const userGetInfo = async () => {
      try {
        const response = await UserApi.userGetUserInfo();
        console.log(response);
        reset(response.data);
        setImgFile((pre) => ({
          ...pre,
          imgSrc: `${process.env.REACT_APP_API_URL}/images/${response.data.imgName}`,
        }));
      } catch (error) {
        console.log(error);
      }
    };
    userGetInfo();
  }, [reset]);

  console.log(getValues("dob"));

  return (
    <>
      <Button
        onClick={() => {
          console.log(getValues());
        }}
      >
        aaa
      </Button>
      <div className="px-1 py-1 table userInfo">
        <Heading title="User Information" />
        <div className="userInfo__body">
          <Form {...layout} onFinish={handleSubmit(handleSubmitForm)}>
            <Row>
              <Col sm={16}>
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
                  name="dob"
                  label="Date of Birth"
                  control={control}
                  errors={errors}
                  type="datePicker"
                  defaultValue={getValues("dob")}
                />
                <CustomField
                  name="branchId"
                  label="Branch"
                  control={control}
                  errors={errors}
                  type="select"
                  options={[
                    { value: "1", name: "1" },
                    { value: "2", name: "2" },
                  ]}
                />
              </Col>
              <Col sm={8}>
                <Row justify="center" style={{ margin: "1rem 0" }}>
                  <Image
                    src={imgFile.imgSrc}
                    width={200}
                    height={200}
                    fallback="https://via.placeholder.com/200"
                  />
                  <input
                    type="file"
                    onChange={loadFileChange}
                    style={{ display: "none" }}
                    ref={fileUpload}
                  />
                </Row>
                <Row justify="center">
                  <Button icon={<UploadOutlined />} onClick={showFileUpload}>
                    Click to upload
                  </Button>
                </Row>
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
