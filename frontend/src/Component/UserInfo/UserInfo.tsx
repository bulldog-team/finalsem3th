import Heading from "../Heading/Heading";
import * as yup from "yup";
import { SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import CustomField from "../Field/Field";
import { UploadOutlined } from "@ant-design/icons";
import { Col, Row, Form, Button, Image, Upload } from "antd";
import { useEffect, useRef, useState } from "react";
import UserApi from "../../helper/axios/userApi";
import { Moment } from "moment";
import BranchApi, { BranchInfoType } from "../../helper/axios/branchApi";

export type UserInfoForm = {
  username: string;
  email: string;
  gender: number;
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

type BranchData = {
  value: number;
  name: string;
};

const UserInfo = () => {
  const initForm: UserInfoForm = {
    username: "",
    email: "",
    gender: 0,
    address: "",
    branchId: 1,
    dob: "",
    phone: "",
  };

  const UserInfoSchema: yup.SchemaOf<UserInfoForm> = yup.object({
    address: yup.string().defined(),
    branchId: yup.number().defined(),
    username: yup.string().defined().required("This field is required!"),
    dob: yup.string().defined(),
    email: yup.string().defined().required("This field is required!"),
    gender: yup.number().integer().defined(),
    phone: yup.string().defined().required("This field is required!"),
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
    console.log(data);
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
    resolver: yupResolver(UserInfoSchema),
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

  const [branchData, setBracnhData] = useState<BranchData[]>();
  const [status, setStatus] = useState<boolean>(false);

  useEffect(() => {
    const userGetInfo = async () => {
      try {
        const response = await UserApi.userGetUserInfo();
        console.log(response.data);
        reset(response.data);
        setStatus(response.data.isAdminAccept);
        setImgFile((pre) => ({
          ...pre,
          imgSrc: `${process.env.REACT_APP_API_URL}/images/${response.data.imgName}`,
        }));
      } catch (error) {
        console.log(error);
      }
    };

    const fetchBranchData = async () => {
      try {
        const response = await BranchApi.getBranchData();
        console.log(response);
        const transferData: BranchData[] = response.data.map((item) => ({
          value: item.branchId,
          name: item.branchName,
        }));
        setBracnhData(transferData);
      } catch (err) {
        console.log(err);
      }
    };
    userGetInfo();
    fetchBranchData();
  }, [reset]);

  return (
    <>
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
                    { value: 0, name: "Female" },
                    { value: 1, name: "Male" },
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
                  options={branchData}
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
                Update
              </Button>
            </Row>
          </Form>
          <Row>
            Please note that your information must be accepted by Admin before
            activating.
          </Row>
          <Row>Status:{`${status}`}</Row>
        </div>
      </div>
    </>
  );
};

export default UserInfo;
