import React, { FC, useState } from "react";
import { useCallback } from "react";
import { Link, NavLink } from "react-router-dom";

import { localStorageService } from "../../helper/localStorage/localStorageService";
import MENU, { MenuType, MenuTypes } from "../Navbar/Menu";

interface SidebarProps {
  menu: MenuTypes[];
}

// Sidebar
const Sidebar: FC<SidebarProps> = (props) => {
  const { menu } = props;

  const [toggleDropdown, setToggleDropdown] = useState<boolean[]>(() => {
    return Array.apply(null, Array(MENU.length)).map((x) => false);
  });

  const handleDropdown = (index: number) => {
    const temp = [...toggleDropdown];
    temp[index] = !temp[index];
    setToggleDropdown(temp);
  };

  const checkAuthorization = useCallback((appRoles) => {
    const userRole = localStorageService.getRole();
    if (appRoles.role && userRole) {
      for (const role of appRoles.role) {
        if (userRole.includes(role)) {
          return true;
        }
      }
    }
    return false;
  }, []);

  return (
    <div className="sidebar overlay-scrollbar">
      <ul className="sidebar-nav">
        {menu.map((item, index) => {
          if (!item.sub) {
            return (
              checkAuthorization(item) && (
                <li className="sidebar-nav--item" key={item.key}>
                  <div className="sidebar-nav--link">
                    {item.link && (
                      <NavLink to={item.link}>
                        <div>
                          <i className="material-icons">{item.icon}</i>
                        </div>
                        <span>{item.title}</span>
                      </NavLink>
                    )}
                  </div>
                </li>
              )
            );
          } else {
            return (
              checkAuthorization(item) && (
                <li className="sidebar-nav--item" key={item.key}>
                  <div className="sidebar-nav--link sidebar-nav--dropdown">
                    <Link to="#" onClick={() => handleDropdown(index)}>
                      <div>
                        <i className="material-icons">{item.icon}</i>
                      </div>
                      <span>{item.title}</span>
                      <span
                        style={{
                          color: "#727475",
                          fontSize: "15px",
                          paddingLeft: "5px",
                        }}
                        className="material-icons"
                      >
                        keyboard_arrow_down
                      </span>
                    </Link>

                    <ul
                      className={`${toggleDropdown[index] ? "appear" : "hide"}`}
                    >
                      {item.sub.map((sub: MenuType) => {
                        return (
                          checkAuthorization(sub) && (
                            <li key={sub.key}>
                              {sub.link && (
                                <NavLink to={sub.link}>{sub.title}</NavLink>
                              )}
                            </li>
                          )
                        );
                      })}
                    </ul>
                  </div>
                </li>
              )
            );
          }
        })}
      </ul>
    </div>
  );
};

export default React.memo(Sidebar);
