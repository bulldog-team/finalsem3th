import { FC, useState } from "react";
import { Button, Divider, Form, List, Typography } from "antd";
import { SubmitHandler, useForm } from "react-hook-form";
import CustomField from "../../Component/Field/Field";
import searchApi, { SearchResponse } from "../../helper/axios/searchApi";
import moment from "moment";

export interface ISearchForm {
  packageId: string;
  pincode: string;
}

const Search: FC = () => {
  const { Title } = Typography;
  const [info, setInfo] = useState<SearchResponse[]>();

  const layout = {
    labelCol: {
      span: 6,
    },
    wrapperCol: {
      span: 13,
    },
  };

  const defaultValues: ISearchForm = {
    packageId: "",
    pincode: "",
  };

  const {
    control,
    formState: { errors },
    handleSubmit,
    reset,
  } = useForm<ISearchForm>({ defaultValues });

  const handleSearch: SubmitHandler<ISearchForm> = async (data) => {
    reset(defaultValues);
    const response = await searchApi.getPackageInfo(data);
    setInfo(response.data);
    console.log(response);
  };

  return (
    <div className="search">
      <div className="search__wrapper">
        <Title style={{ textAlign: "center" }}> Search</Title>
        <div className="search__form">
          <Form
            {...layout}
            onFinish={handleSubmit(handleSearch)}
            style={{ textAlign: "center" }}
          >
            <CustomField
              name="packageId"
              control={control}
              label="PackageId"
              type="text"
              errors={errors}
            />
            <CustomField
              name="pincode"
              control={control}
              label="Pincode"
              type="text"
              errors={errors}
            />
            <Button type="primary" htmlType="submit">
              Search
            </Button>
          </Form>
        </div>
        <Divider orientation="center"></Divider>
        <div className="search__result" />
        {info && (
          <List
            itemLayout="vertical"
            size="default"
            bordered
            dataSource={info}
            renderItem={(item) => (
              <List.Item style={{ textAlign: "center" }}>
                Package Id: {item.packageId} <br />
                Package Status: {item.packageStatus} <br />
                Delivery Type: {item.packageType} <br />
                Pincode: {item.pincode} <br />
                Sender's Address: {item.senderAddress} <br />
                Date Sent: {moment(item.dateSent).format("YYYY-MM-DD")} <br />
                Receiver's Address: {item.receiveAddress} <br />
                Date Receive:
                {item.dateReceived
                  ? moment(item.dateReceived).format("YYYY-MM-DD")
                  : "Not yet"}
                <br />
                Distance : {item.distance} <br />
                Weight: {item.weight} <br />
                Payment Status: {item.isPaid ? "Paid" : "Not yet"}
                <br />
              </List.Item>
            )}
          />
        )}
      </div>
    </div>
  );
};

export default Search;