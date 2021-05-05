import { FC, useState } from "react";
import { Row, Typography } from "antd";
import { ThunderboltOutlined } from "@ant-design/icons";

import LoginPage from "./Login";
import { useSelector } from "react-redux";
import { AppState } from "../../store/store";
import { RedirectProps, Redirect } from "react-router-dom";

type ExtendRouteProps = RedirectProps & {
  location: {
    state: {
      from: string;
    };
  };
};

// Authenticate Page
const AuthPage: FC<ExtendRouteProps> = (props) => {
  const { Title } = Typography;

  const [redirectToReferrer, setRedirectToReferrer] = useState<boolean>(false);

  const { from } = props.location.state || {
    from: { pathname: "/" },
  };

  const auth = useSelector((state: AppState) => state.auth);
  const isAuthenticate = auth.token ? true : false;

  if (redirectToReferrer || isAuthenticate) {
    return <Redirect to={from} />;
  }

  return (
    <div className="auth">
      <div className="auth__wrapper">
        <Title style={{ textAlign: "center" }}> Welcome</Title>
        <Row className="auth__logo">
          <ThunderboltOutlined style={{ fontSize: "80px" }} />
        </Row>
        <div className="auth__form">
          <LoginPage setRedirectToReferrer={setRedirectToReferrer} />
        </div>
      </div>
    </div>
  );
};

export default AuthPage;
