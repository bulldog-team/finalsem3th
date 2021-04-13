import { Modal } from "antd";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import UserApi, { UserInfoType } from "../../helper/axios/userApi";

interface UserModalProps {
  isModalOpen: boolean;
  setIsModalOpen: Dispatch<SetStateAction<boolean>>;
  userId: number | undefined;
}

const handleOk = () => {};

const handleCancel = () => {};

const UserModal: FC<UserModalProps> = (props) => {
  const { isModalOpen, setIsModalOpen, userId } = props;

  const [userInfo, setUserInfo] = useState<UserInfoType>();
  console.log(userId);

  useEffect(() => {
    const fetchUserInfo = async () => {
      const response = await UserApi.adminGetUserInfo(userId);
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
    >
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
    </Modal>
  );
};

export default UserModal;
