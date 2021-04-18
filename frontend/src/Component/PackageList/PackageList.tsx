import { Button, message, Popconfirm, Row, Space, Table, Tooltip } from "antd";
import { ExportOutlined, RestOutlined, PlusOutlined } from "@ant-design/icons";

import { useEffect, useState } from "react";
import packageApi, { PackageListType } from "../../helper/axios/packageApi";
import Heading from "../Heading/Heading";

type PackageListDataSource = PackageListType & {
  key: number;
};

const PackageList = () => {
  const [packageList, setPackageList] = useState<PackageListDataSource[]>();
  const [isViewModalOpen, setIsViewModalOpen] = useState<boolean>(false);
  const [name, setName] = useState<number>();
  const [update, setUpdate] = useState<boolean>(false);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState<boolean>(false);

  const { Column } = Table;

  useEffect(() => {
    const fetchDaTa = async () => {
      try {
        const response = await packageApi.getPackageList();
        const transferData: PackageListDataSource[] = response.data.map(
          (item, index) => ({ ...item, key: index + 1 })
        );
        setPackageList(transferData);
        console.log(response.data);
      } catch (err) {
        console.log(err);
        message.error(err.response.data, 5);
      }
    };
    fetchDaTa();
  }, [update]);

  return (
    <>
      <div className="px-1 py-1 table userList">
        <Heading title="Package List" />
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
          <Table dataSource={packageList}>
            <Column title="#" dataIndex="key" />
            <Column
              title="Sender Name"
              dataIndex="senderName"
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.senderName
                  .toString()
                  .localeCompare(b.senderName.toString());
              }}
            />
            <Column
              title="Receive Name"
              dataIndex="receiveName"
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.receiveName
                  .toString()
                  .localeCompare(b.receiveName.toString());
              }}
            />
            <Column
              title="Date Sent"
              dataIndex="dateSent"
              render={(data) => {
                return <>{data ? "Activated" : "Not Activated"}</>;
              }}
              // sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
              //   return a.isAdminAccept
              //     .toString()
              //     .localeCompare(b.isAdminAccept.toString());
              // }}
            />
            <Column
              title="Status"
              dataIndex="isPaid"
              render={(data) => {
                return <>{data ? "Activated" : "Not Activated"}</>;
              }}
              // sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
              //   return a.isAdminAccept
              //     .toString()
              //     .localeCompare(b.isAdminAccept.toString());
              // }}
            />
            <Column
              title="Action"
              dataIndex="totalPrice"
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.totalPrice - b.totalPrice;
              }}
            />
            <Column
              title="Status"
              render={(_, record: PackageListDataSource) => {
                return (
                  <Space size="middle">
                    <Tooltip title="View">
                      <ExportOutlined
                        className="ant-icon icon-primary userList__btn"
                        onClick={() => {
                          setIsViewModalOpen(true);
                        }}
                      />
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

export default PackageList;
