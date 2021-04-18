import { Button, Form, message, Row } from "antd";

import { useEffect, useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import packageApi, {
  DeliveryType,
  DeliveryTypeUpdate,
} from "../../helper/axios/packageApi";
import CustomField from "../Field/Field";
import Heading from "../Heading/Heading";

const PackagePriceList = () => {
  const [listPrice, setListPrice] = useState<DeliveryType[]>([]);

  interface IForm {
    [x: string]: number;
  }

  const {
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = useForm<IForm>();

  useEffect(() => {
    const fetchPriceList = async () => {
      const response = await packageApi.getPriceList();
      setListPrice(response.data);
      const defaultValues = response.data.reduce((cur, item) => {
        return {
          ...cur,
          [item.typeName]: item.unitPrice,
        };
      }, {});
      reset(defaultValues);
    };
    fetchPriceList();
  }, [reset]);

  const layout = {
    labelCol: {
      span: 8,
      offset: 1,
    },
    wrapperCol: {
      span: 14,
    },
  };

  const handleUpdatePriceList: SubmitHandler<IForm> = async (data) => {
    const request: DeliveryTypeUpdate[] = [];
    for (let [key, value] of Object.entries(data)) {
      request.push({
        typeName: key,
        unitPrice: value,
      });
    }
    await packageApi.updatePriceList(request);
    message.success("Completely save all changes!");
  };

  return (
    <>
      <div className="px-1 py-1 table">
        <Heading title="Update Price List" />
        <Form onFinish={handleSubmit(handleUpdatePriceList)} {...layout}>
          {listPrice.map((item: DeliveryType) => {
            return (
              <CustomField
                key={item.typeName}
                name={item.typeName}
                control={control}
                errors={errors}
                label={item.typeName}
                placeholder={item.typeName}
                type="number"
                defaultValue={item.unitPrice}
              />
            );
          })}
          <Row justify="center">
            <Button key="submit" htmlType="submit" type="primary">
              Update
            </Button>
          </Row>
        </Form>
      </div>
    </>
  );
};

export default PackagePriceList;
