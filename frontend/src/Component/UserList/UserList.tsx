import { Button, message, Popconfirm, Row, Space, Table, Tooltip } from "antd";
import { ExportOutlined, RestOutlined, PlusOutlined } from "@ant-design/icons";

import { useEffect, useState } from "react";
import UserApi, { UserListType } from "../../helper/axios/userApi/index";
import Heading from "../Heading/Heading";
import ViewUserInfo from "./ViewUserInfo";
import CreateUserModal from "./CreateUserModal";

type UserListDataSource = UserListType & {
  key: number;
  userId: number;
};

// View all user in list
const UserList = () => {
  const [userListData, setUserListData] = useState<UserListDataSource[]>();
  const [isViewModalOpen, setIsViewModalOpen] = useState<boolean>(false);
  const [name, setName] = useState<number>();
  const [update, setUpdate] = useState<boolean>(false);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState<boolean>(false);

  const { Column } = Table;

  useEffect(() => {
    const fetchDaTa = async () => {
      try {
        const response = await UserApi.getUserList();
        const transferData: UserListDataSource[] = response.data.map(
          (item, index) => ({ ...item, key: index + 1, userId: item.userId })
        );
        setUserListData(transferData);
        console.log(response.data);
      } catch (err) {
        console.log(err);
        message.error(err.response.data, 5);
      }
    };
    fetchDaTa();
  }, [update]);

  const handleDeleteUser = async (record: UserListDataSource) => {
    try {
      await UserApi.adminDeleteUser(record.userId);
      message.success("This account has been deleted succesfull");
      setUpdate((pre) => !pre);
    } catch (error) {
      message.error(error.response.data, 5);
    }
  };

  return (
    <>
      {isCreateModalOpen && (
        <CreateUserModal
          isCreateModalOpen={isCreateModalOpen}
          setIsCreateModalOpen={setIsCreateModalOpen}
          setUpdate={setUpdate}
        />
      )}
      {isViewModalOpen && (
        <ViewUserInfo
          isModalOpen={isViewModalOpen}
          setIsModalOpen={setIsViewModalOpen}
          userId={name}
          setUpdate={setUpdate}
        />
      )}
      <div className="px-1 py-1 table userList">
        <Heading title="User List" />
        <div className="userInfo__body">
          <Row justify="end" style={{ margin: "1rem 0" }}>
            <Button
              onClick={() => setIsCreateModalOpen((pre) => !pre)}
              icon={<PlusOutlined />}
              className="btn-success"
            >
              New
            </Button>
          </Row>
          <Table dataSource={userListData}>
            <Column title="#" dataIndex="key" />
            <Column
              title="Username"
              dataIndex="username"
              sorter={(a: UserListDataSource, b: UserListDataSource) => {
                return a.username
                  .toString()
                  .localeCompare(b.username.toString());
              }}
            />
            <Column
              title="Branch"
              dataIndex="branch"
              sorter={(a: UserListDataSource, b: UserListDataSource) => {
                return a.branch.toString().localeCompare(b.branch.toString());
              }}
            />
            <Column
              title="Profile Status"
              dataIndex="isAdminAccept"
              render={(data) => {
                return <>{data ? "Activated" : "Not Activated"}</>;
              }}
              sorter={(a: UserListDataSource, b: UserListDataSource) => {
                return a.isAdminAccept
                  .toString()
                  .localeCompare(b.isAdminAccept.toString());
              }}
            />
            <Column
              title="Action"
              render={(_, record: UserListDataSource) => {
                return (
                  <Space size="middle">
                    <Tooltip title="View">
                      <ExportOutlined
                        className="ant-icon icon-primary userList__btn"
                        onClick={() => {
                          setIsViewModalOpen(true);
                          setName(record.userId);
                        }}
                      />
                    </Tooltip>
                    <Tooltip title="Delete">
                      <Popconfirm
                        title="Are you sure to delete this user?"
                        onConfirm={() => handleDeleteUser(record)}
                      >
                        <RestOutlined className="ant-icon icon-danger userList__btn" />
                      </Popconfirm>
                    </Tooltip>
                  </Space>
                );
              }}
            />
          </Table>
        </div>
      </div>
    </>
  );
};

export default UserList;
