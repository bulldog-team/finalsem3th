import { Button, Form, message, Modal } from "antd";
import { FC, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { useDispatch } from "react-redux";
import UserApi from "../../../helper/axios/userApi";
import { localStorageService } from "../../../helper/localStorage/localStorageService";

import * as actionCreator from "../../../store/action/index";
import CustomField from "../../Field/Field";

interface UserMenuProps {
  id: string;
  index: number;
  icon: string;
  active: boolean;
  tabIndex: number;
  toggleRightMenu: (index: number) => void;
}

type RightMenuType = {
  key: string;
  icon: string;
  text: string;
};

interface IChangePasswordForm {
  currentPassword: string;
  password: string;
  confirmPassword: string;
}

// User menu component
const UserMenu: FC<UserMenuProps> = (props) => {
  const { id, index, icon, active, tabIndex, toggleRightMenu } = props;

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  const dispatch = useDispatch();

  const handleUserMenuClick = (item: RightMenuType) => {
    switch (item.key) {
      case "clear":
        localStorageService.clearAll();
        return dispatch(actionCreator.authLogout());

      case "lock":
        return setIsModalOpen(true);

      default:
        break;
    }
  };

  const USER_MENU = [
    { key: "lock", icon: "lock", text: "Change Password" },
    { key: "clear", icon: "clear", text: "Log out" },
  ];

  const defaultValues: IChangePasswordForm = {
    confirmPassword: "",
    currentPassword: "",
    password: "",
  };

  const {
    control,
    formState: { errors },
    handleSubmit,
    reset,
  } = useForm<IChangePasswordForm>({
    defaultValues,
  });

  const layout = {
    labelCol: {
      span: 8,
      offset: 1,
    },
    wrapperCol: {
      span: 13,
      offset: 1,
    },
  };

  const handdleChangePassword: SubmitHandler<IChangePasswordForm> = async (
    data
  ) => {
    try {
      await UserApi.updatePassword(data);
      reset();
      setIsModalOpen(false);
      message.success("Successfully changed password!", 5);
    } catch (error) {
      message.error(error.response.data, 5);
    }
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <>
      <Modal
        title="Change password"
        visible={isModalOpen}
        footer={[
          <Button key="back" onClick={handleCloseModal}>
            Close
          </Button>,
          <Button
            key="submit"
            htmlType="submit"
            type="primary"
            form="changePasswordForm"
          >
            Update Password
          </Button>,
        ]}
      >
        <Form
          {...layout}
          onFinish={handleSubmit(handdleChangePassword)}
          id="changePasswordForm"
        >
          <CustomField
            name="currentPassword"
            label="Current Password"
            control={control}
            errors={errors}
            type="password"
          />
          <CustomField
            name="password"
            label="New Password"
            control={control}
            errors={errors}
            type="password"
          />
          <CustomField
            name="confirmPassword"
            label="Confirm Password"
            control={control}
            errors={errors}
            type="password"
          />
        </Form>
      </Modal>
      <li className="nv--item owdropdown avt-wrapper">
        <div className="avt" id={id}>
          <span
            className="material-icons"
            tabIndex={tabIndex}
            onClick={() => {
              toggleRightMenu(index);
            }}
            onBlur={() => toggleRightMenu(index)}
          >
            {icon}
          </span>

          <ul
            className={`owdropdown-menu ${active ? "owdropdown-expand" : ""}`}
          >
            {USER_MENU.map((item) => {
              return (
                <li key={item.key} className="owdropdown-menu--item">
                  <div
                    onClick={() => handleUserMenuClick(item)}
                    className="owdropdown-menu--link"
                  >
                    <div>
                      <span className="material-icons">{item.icon}</span>
                    </div>
                    <span className="owdropdown-menu--text">{item.text}</span>
                  </div>
                </li>
              );
            })}
          </ul>
        </div>
      </li>
    </>
  );
};

export default UserMenu;
