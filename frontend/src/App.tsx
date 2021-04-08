import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { BrowserRouter, Redirect, Route, Switch } from "react-router-dom";
import Loading from "./Component/Loading/Loading";

import PrivateRoute from "./Component/PrivateRoute/PrivateRoute";
import Role from "./helper/config/Role";
import { localStorageService } from "./helper/localStorage/localStorageService";
import AuthPage from "./Page/Auth/Auth";
import Dashboard from "./Page/Dashboard/Dashboard";
import * as actionCreator from "./store/action/index";
import { AppState } from "./store/store";

const App = () => {
  const dispatch = useDispatch();

  useEffect(() => {
    if (!localStorageService.getToken()) {
      return localStorageService.clearAll();
    }
    dispatch(actionCreator.handleAutoLogin());
  }, [dispatch]);

  const auth = useSelector((state: AppState) => state.auth);
  if (auth.loading) {
    return <Loading />;
  }

  const routes = (
    <Switch>
      <Route path="/auth" component={AuthPage} />
      <PrivateRoute
        path="/"
        component={Dashboard}
        requiredRole={[Role.USER, Role.ADMIN]}
      />
      <Redirect to="/auth" />
    </Switch>
  );

  return <BrowserRouter>{routes}</BrowserRouter>;
};

export default App;
