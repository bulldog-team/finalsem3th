import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { BrowserRouter, Redirect, Route, Switch } from "react-router-dom";

import PrivateRoute from "./Component/PrivateRoute/PrivateRoute";
import Role from "./helper/config/Role";
import { localStorageService } from "./helper/localStorage/localStorageService";
import AuthPage from "./Page/Auth/Auth";
import Dashboard from "./Page/Dashboard/Dashboard";
import * as actionCreator from "./store/action/index";

const App = () => {
  const dispatch = useDispatch();

  console.log("check");
  useEffect(() => {
    if (!localStorageService.getAcToken()) {
      return localStorageService.clearAll();
    }
    dispatch(actionCreator.handleAutoLogin());
  }, [dispatch]);

  const routes = (
    <Switch>
      <Route path="/auth" component={AuthPage} />
      <PrivateRoute path="/" component={Dashboard} requiredRole={[Role.USER]} />
      <Redirect to="/auth" />
    </Switch>
  );

  return <BrowserRouter>{routes}</BrowserRouter>;
};

export default App;
