import { Button, message, Popconfirm, Row, Space, Table, Tooltip } from "antd";
import {
  ExportOutlined,
  PlusOutlined,
  RestOutlined,
  EditOutlined,
} from "@ant-design/icons";

import { useEffect, useState } from "react";
import packageApi, { PackageListType } from "../../helper/axios/packageApi";
import Heading from "../Heading/Heading";
import moment from "moment";
import ViewPackageInfo from "./ViewPackageInfo";
import CreatePackage from "./CreatePackage";
import { localStorageService } from "../../helper/localStorage/localStorageService";
import EditPackageModal from "./EditPackage";

type PackageListDataSource = PackageListType & {
  key: number;
};

// View all packages in list
const PackageList = () => {
  const [packageList, setPackageList] = useState<PackageListDataSource[]>();
  const [isViewModalOpen, setIsViewModalOpen] = useState<boolean>(false);
  const [packageId, setPackageId] = useState<number>();
  const [update, setUpdate] = useState<boolean>(false);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState<boolean>(false);
  const [isEditModal, setIsEditModal] = useState<boolean>();

  const getRole = localStorageService.getRole();
  const isAdmin = getRole?.includes("Admin");

  const { Column } = Table;

  const handleDeletePackage = async (record: PackageListDataSource) => {
    try {
      await packageApi.adminDeletePackage(record.packageId);
      message.success("This package has been deleted succesfull");
      setUpdate((pre) => !pre);
    } catch (error) {
      message.error(error.response.data, 5);
    }
  };

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
      {isEditModal && packageId && (
        <EditPackageModal
          isEditModal={isEditModal}
          packageId={packageId}
          setIsEditModal={setIsEditModal}
          setUpdate={setUpdate}
        />
      )}
      {isViewModalOpen && packageId && (
        <ViewPackageInfo
          isViewModalOpen={isViewModalOpen}
          packageId={packageId}
          setIsViewModalOpen={setIsViewModalOpen}
          setUpdate={setUpdate}
        />
      )}
      {isCreateModalOpen && (
        <CreatePackage
          isCreateModalOpen={isCreateModalOpen}
          setIsCreateModalOpen={setIsCreateModalOpen}
          setUpdate={setUpdate}
        />
      )}
      <div className="px-1 py-1 table packageList">
        <Heading title="Package List" />
        <div className="packageList__body">
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
                return moment(data).format("YYYY-MM-DD");
              }}
            />
            <Column
              title="Type"
              dataIndex="type"
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.type.localeCompare(b.type);
              }}
            />
            <Column
              title="Status"
              dataIndex="isPaid"
              render={(data) => {
                return <>{data ? "Paid" : "Not yet"}</>;
              }}
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.isPaid.toString().localeCompare(b.isPaid.toString());
              }}
            />
            <Column
              title="Price"
              dataIndex="totalPrice"
              sorter={(a: PackageListDataSource, b: PackageListDataSource) => {
                return a.totalPrice - b.totalPrice;
              }}
            />
            <Column
              title="Action"
              render={(_, record: PackageListDataSource) => {
                return (
                  <Space size="middle">
                    {record.packageStatus === "Picked up" ? (
                      <Tooltip title="Edit">
                        <EditOutlined
                          className="ant-icon icon-warning packageList__btn"
                          onClick={() => {
                            setIsEditModal(true);
                            setPackageId(record.packageId);
                            console.log(record.packageStatus);
                          }}
                        />
                      </Tooltip>
                    ) : (
                      <EditOutlined
                        className="ant-icon icon-disable packageList__btn--disable"
                        disabled
                      />
                    )}

                    <Tooltip title="Payment">
                      <ExportOutlined
                        className="ant-icon icon-primary packageList__btn"
                        onClick={() => {
                          setIsViewModalOpen(true);
                          setPackageId(record.packageId);
                        }}
                      />
                    </Tooltip>
                    {isAdmin && (
                      <Tooltip title="Delete">
                        <Popconfirm
                          title="Are you sure to delete this user?"
                          onConfirm={() => handleDeletePackage(record)}
                        >
                          <RestOutlined className="ant-icon icon-danger userList__btn" />
                        </Popconfirm>
                      </Tooltip>
                    )}
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
