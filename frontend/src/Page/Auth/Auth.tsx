import { FC, useState } from "react";
import { Row, Typography } from "antd";

import LoginPage from "./Login";
import RegisterPage from "./Register";
import { useTranslation } from "react-i18next";
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

const AuthPage: FC<ExtendRouteProps> = (props) => {
  const { Title } = Typography;
  const { t } = useTranslation();

  const [redirectToReferrer, setRedirectToReferrer] = useState<boolean>(false);
  const [isLoginMode, setIsLoginMode] = useState<boolean>(true);

  const { from } = props.location.state || {
    from: { pathname: "/" },
  };

  const auth = useSelector((state: AppState) => state.auth);
  const isAuthenticate = auth.acToken ? true : false;

  if (redirectToReferrer || isAuthenticate) {
    return <Redirect to={from} />;
  }

  const handleChangeMode = () => setIsLoginMode((pre) => !pre);

  return (
    <div className="auth">
      <div className="auth__wrapper">
        <Title style={{ textAlign: "center" }}> Welcome</Title>
        <Row className="auth__logo">Logo</Row>
        <div className="auth__form">
          {isLoginMode ? (
            <LoginPage setRedirectToReferrer={setRedirectToReferrer} />
          ) : (
            <RegisterPage />
          )}
        </div>
        <Row className="auth__changeMode">
          {isLoginMode ? t("Auth.dontHaveAccout") : t("Auth.haveAccount")}
          <span className="auth__change" onClick={handleChangeMode}>
            &ensp;
            {isLoginMode ? t("Auth.signUp") : t("Auth.login")}
          </span>
        </Row>
      </div>
    </div>
  );
};

export default AuthPage;
