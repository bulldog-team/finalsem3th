import { FC, useCallback, useState } from "react";
import { Switch, Route, Redirect } from "react-router-dom";

import UserInfo from "../../Component/UserInfo/UserInfo";
import UserList from "../../Component/UserList/UserList";
import PrivateRoute from "../../Component/PrivateRoute/PrivateRoute";
import Role from "../../helper/config/Role";
import ErrorPage from "../Error/ErrorPage";
import Navbar from "../../Component/Navbar/Navbar";
import Sidebar from "../../Component/Sidebar/Sidebar";
import MENU from "../../Component/Navbar/Menu";
import PackageList from "../../Component/PackageList/PackageList";

const Dashboard: FC = () => {
  const [isSidebarOpen, SetIsSidebarOpen] = useState<boolean>(false);

  const toggleSidebar = useCallback(() => {
    SetIsSidebarOpen((pre) => !pre);
  }, []);

  const routes = (
    <Switch>
      <PrivateRoute
        path={`/user-list`}
        component={UserList}
        exact
        requiredRole={[Role.ADMIN]}
      />
      <PrivateRoute
        path={`/package-list`}
        exact
        requiredRole={[Role.ADMIN, Role.USER]}
        component={PackageList}
      />
      <PrivateRoute
        path={"/user-info"}
        exact
        requiredRole={[Role.ADMIN, Role.USER]}
        component={UserInfo}
      />
      <Route path="/error" exact component={ErrorPage} />
      <Redirect to="/user-info" />
    </Switch>
  );

  return (
    <div
      className={`overlay-scrollbar ${
        isSidebarOpen ? "sidebar-expand" : "sidebar-close"
      }`}
    >
      <Navbar toggleSidebar={toggleSidebar} />
      <Sidebar menu={MENU} />
      <div className="wrapper">
        <div className="table">{routes}</div>
      </div>
    </div>
  );
};

export default Dashboard;
