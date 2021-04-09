import { Row, Col, Typography } from "antd";
import { FC } from "react";

interface HeadingProps {
  title: string;
}

const Heading: FC<HeadingProps> = (props) => {
  const { Title } = Typography;
  const { title } = props;
  return (
    <Row>
      <Col>
        <Title level={3}>{title}</Title>
      </Col>
    </Row>
  );
};

export default Heading;
