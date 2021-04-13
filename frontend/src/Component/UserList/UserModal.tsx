import { Modal } from "antd";
import { Dispatch, FC, SetStateAction } from "react";

interface UserModalProps {
  isModalOpen: boolean;
  setIsModalOpen: Dispatch<SetStateAction<boolean>>;
  username: string | undefined;
}

const handleOk = () => {};

const handleCancel = () => {};

const UserModal: FC<UserModalProps> = (props) => {
  const { isModalOpen, setIsModalOpen, username } = props;
  return (
    <Modal
      title="Basic Modal "
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
