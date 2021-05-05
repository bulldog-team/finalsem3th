import { Result } from "antd";
import { FC } from "react";
import { useSelector } from "react-redux";
import { Redirect, Route, RouteProps } from "react-router-dom";
import { localStorageService } from "../../helper/localStorage/localStorageService";

import { AppState } from "../../store/store";

type PrivateRouteProps = RouteProps & {
  component: React.ComponentType<any>;
  requiredRole: string[];
  path: string;
};

interface ICheckRole {
  (requiredRole: string[]): boolean;
}

// Protected route component
const PrivateRoute: FC<PrivateRouteProps> = ({
  component: Component,
  requiredRole,
  ...rest
}) => {
  const auth = useSelector((state: AppState) => state.auth);
  const isAuthenticate = auth.token ? true : false;

  const checkRole: ICheckRole = (requiredRole: string[]) => {
    const userRole = localStorageService.getRole();
    if (userRole) {
      return userRole.some((role) => requiredRole.includes(role));
    }
    return false;
  };

  return (
    <Route
      {...rest}
      render={(props) => {
        if (!isAuthenticate) {
          return (
            <Redirect
              to={{ pathname: "/auth", state: { from: props.location } }}
            />
          );
        }
        if (isAuthenticate && checkRole(requiredRole)) {
          return <Component {...props} />;
        }

        return (
          <Result
            status="403"
            title="403"
            subTitle="Sorry, you are not authorized to access this page."
          />
        );
      }}
    />
  );
};

export default PrivateRoute;
