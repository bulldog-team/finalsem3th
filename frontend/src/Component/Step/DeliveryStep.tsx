import { FC } from "react";

import { Steps } from "antd";

export interface IStepProps {
  current: string;
}

const DeliveryStep: FC<IStepProps> = (props) => {
  const { current } = props;
  const { Step } = Steps;

  const getCurrentStep = (current: string): number => {
    console.log(current);
    switch (current) {
      case "Picked up":
        return 0;
      case "Sending":
        return 1;
      case "Received":
        return 2;
      default:
        return 0;
    }
  };

  return (
    <div style={{ margin: "1rem" }}>
      <Steps current={getCurrentStep(current)}>
        <Step title="Picked up" description="Picked up" />
        <Step title="Sending" description="Sending" />
        <Step title="Received" description="Received" />
      </Steps>
    </div>
  );
};

export default DeliveryStep;
