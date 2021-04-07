import { Button, Result } from "antd";

const VerifyPage = () => {
  return (
    <Result
      status="success"
      title={"Verify.confirm"}
      extra={<Button type="primary">{"Verify.return"}</Button>}
    />
  );
};

export default VerifyPage;
