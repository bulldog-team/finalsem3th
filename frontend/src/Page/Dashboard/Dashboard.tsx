import { FC } from "react";
import { Switch, Route, Redirect, useRouteMatch } from "react-router-dom";
import Intro from "../../Component/Intro/Intro";
import Intro1 from "../../Component/Intro1/Intro1";
import PrivateRoute from "../../Component/PrivateRoute/PrivateRoute";
import Role from "../../helper/config/Role";
import ErrorPage from "../Error/ErrorPage";

const Dashboard: FC = () => {
  const match = useRouteMatch();
  const routes = (
    <Switch>
      <PrivateRoute
        path={`${match.path}/intro`}
        component={Intro1}
        exact
        requiredRole={[Role.USER]}
      />
      <PrivateRoute
        path={`${match.path}/`}
        exact
        requiredRole={[Role.ADMIN, Role.USER]}
        component={Intro}
      />
      <Route path="/error" exact component={ErrorPage} />
      <Redirect to="/error" />
    </Switch>
  );
  return <>{routes} </>;
};

export default Dashboard;
