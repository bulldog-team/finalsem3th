import { Button, Col, Descriptions, Image, Modal, Row } from "antd";
import moment from "moment";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import UserApi, { UserInfoType } from "../../helper/axios/userApi";

interface UserModalProps {
  isModalOpen: boolean;
  setIsModalOpen: Dispatch<SetStateAction<boolean>>;
  userId: number | undefined;
}

const UserModal: FC<UserModalProps> = (props) => {
  const { isModalOpen, setIsModalOpen, userId } = props;

  const [userInfo, setUserInfo] = useState<UserInfoType>();
  console.log(userId);

  const handleOk = async () => {
    const response = await UserApi.adminUpdateUserInfo(userId);
    console.log(response);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  useEffect(() => {
    const fetchUserInfo = async () => {
      const response = await UserApi.adminGetUserInfo(userId);
      setUserInfo(response.data);
      console.log(response);
    };
    fetchUserInfo();
  }, [userId]);

  return (
    <Modal
      title="User Information"
      visible={isModalOpen}
      onOk={handleOk}
      onCancel={handleCancel}
      width={960}
      footer={[
        <Button key="back" onClick={handleCancel}>
          Return
        </Button>,
        <Button key="submit" type="primary" onClick={handleOk}>
          Approve
        </Button>,
      ]}
    >
      <Descriptions bordered>
        <Descriptions.Item label="Username">
          {userInfo?.username}
        </Descriptions.Item>
        <Descriptions.Item label="Email">{userInfo?.email}</Descriptions.Item>
        <Descriptions.Item label="Gender">
          {userInfo?.gender === 1 ? "Male" : "Female"}
        </Descriptions.Item>
        <Descriptions.Item label="Address">
          {userInfo?.address}
        </Descriptions.Item>
        <Descriptions.Item label="Date of Birth">
          {moment(userInfo?.dob).format("YYYY-MM-DD")}
        </Descriptions.Item>
        <Descriptions.Item label="Branch Name">
          {userInfo?.branchName}
        </Descriptions.Item>
        <Descriptions.Item label="Phone">{userInfo?.phone}</Descriptions.Item>
        <Descriptions.Item label="Avatar">
          <Row justify="center">
            <Image
              style={{ justifyContent: "center" }}
              src={`${process.env.REACT_APP_API_URL}/images/${userInfo?.imgName}`}
            />
          </Row>
        </Descriptions.Item>
      </Descriptions>
    </Modal>
  );
};

export default UserModal;