import { Button, Row, Space, Table, Tooltip } from "antd";
import { ExportOutlined, RestOutlined, PlusOutlined } from "@ant-design/icons";

import { useEffect, useState } from "react";
import UserApi, { UserListType } from "../../helper/axios/userApi/index";
import Heading from "../Heading/Heading";
import UserModal from "./UserModal";

type UserListDataSource = UserListType & {
  key: number;
};

const UserList = () => {
  const [userListData, setUserListData] = useState<UserListDataSource[]>();
  const [isViewModalOpen, setIsViewModalOpen] = useState<boolean>(false);
  const [name, setName] = useState<string>();

  const { Column } = Table;

  useEffect(() => {
    const fetchDaTa = async () => {
      try {
        const response = await UserApi.getUserList();
        const transferData: UserListDataSource[] = response.data.map(
          (item, index) => ({ ...item, key: index + 1 })
        );
        setUserListData(transferData);
        console.log(response.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchDaTa();
  }, []);

  return (
    <>
      {isViewModalOpen && (
        <UserModal
          isModalOpen={isViewModalOpen}
          setIsModalOpen={setIsViewModalOpen}
          username={name}
        />
      )}
      <div className="px-1 py-1 table userList">
        <Heading title="User List" />
        <div className="userInfo__body">
          <Row justify="end" style={{ margin: "1rem 0" }}>
            <Button icon={<PlusOutlined />} className="btn-success">
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
                console.log(data.toString());
                return <>{data ? "Activated" : "Not Activate"}</>;
              }}
              sorter={(a: UserListDataSource, b: UserListDataSource) => {
                return a.isAdminAccept
                  .toString()
                  .localeCompare(b.isAdminAccept.toString());
              }}
            />
            <Column
              title="Action"
              render={(_, record) => {
                return (
                  <Space size="middle">
                    <Tooltip title="View">
                      <ExportOutlined
                        className="ant-icon icon-primary userList__btn"
                        onClick={() => setIsViewModalOpen(true)}
                      />
                    </Tooltip>
                    <Tooltip title="Delete">
                      <RestOutlined className="ant-icon icon-danger userList__btn" />
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
