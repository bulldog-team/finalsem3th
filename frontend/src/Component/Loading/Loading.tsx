import { Spin } from "antd";

// Loading component
const Loading = () => {
  return (
    <div className="loading">
      <Spin size="large" className="spin" />
    </div>
  );
};

export default Loading;
