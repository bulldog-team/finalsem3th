import Modal from "antd/lib/modal/Modal";
import { Dispatch, FC, SetStateAction, useEffect, useState } from "react";
import packageApi, { PackageInfo } from "../../helper/axios/packageApi";

interface ViewpackageInfoProps {
  isViewModalOpen: boolean;
  setIsViewModalOpen: Dispatch<SetStateAction<boolean>>;
  packageId: number;
  setUpdate: Dispatch<SetStateAction<boolean>>;
}

const ViewPackageInfo: FC<ViewpackageInfoProps> = (props) => {
  const { isViewModalOpen, setIsViewModalOpen, packageId, setUpdate } = props;

  const [packageInfo, setPackageInfo] = useState<PackageInfo>();

  const handleOk = async () => {};

  const handleCancel = () => {
    setIsViewModalOpen(false);
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await packageApi.userGetPackageInfo(packageId);
      setPackageInfo(response.data);
      console.log(response);
    };
    fetchData();
  }, [packageId]);
  return (
    <Modal
      title="Package Info"
      visible={isViewModalOpen}
      onOk={handleOk}
      onCancel={handleCancel}
      width={960}
    ></Modal>
  );
};

export default ViewPackageInfo;
