import { useCallback, useState, FC } from "react";
import { ThunderboltOutlined } from "@ant-design/icons";
import UserMenu from "./UserMenu/UserMenu";

interface IPropsType {
  toggleSidebar: React.MouseEventHandler<HTMLSpanElement>;
}

const Navbar: FC<IPropsType> = (props) => {
  const { toggleSidebar } = props;

  const [handleRightMenu, setHandleRightMenu] = useState([
    {
      id: "notiMenu",
      index: 0,
      tabIndex: 0,
      icon: "notifications",
      active: false,
    },
    {
      id: "userMenu",
      index: 1,
      tabIndex: 1,
      icon: "account_circle",
      active: false,
    },
  ]);

  const toggleRightMenu = useCallback(
    (index) => {
      setHandleRightMenu(
        [...handleRightMenu].map((item) => {
          if (item.index === index) {
            return {
              ...item,
              active: !item.active,
            };
          } else
            return {
              ...item,
              active: false,
            };
        })
      );
    },
    [handleRightMenu]
  );

  return (
    <nav>
      <div className="nvbar">
        <ul className="nvbar--nav">
          <li className="nv--item">
            <div className="nv--link">
              <span className="material-icons" onClick={toggleSidebar}>
                menu
              </span>
            </div>
          </li>
          <li className="nv--item">
            <ThunderboltOutlined className="logo" />
          </li>
        </ul>
        <ul className="nvbar--nav nv--right">
          <UserMenu
            id={handleRightMenu[1].id}
            index={handleRightMenu[1].index}
            tabIndex={handleRightMenu[1].tabIndex}
            icon={handleRightMenu[1].icon}
            active={handleRightMenu[1].active}
            toggleRightMenu={toggleRightMenu}
          />
        </ul>
      </div>
    </nav>
  );
};

export default Navbar;
