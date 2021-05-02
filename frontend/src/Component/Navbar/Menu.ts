import { title } from "node:process";
import Role from "../../helper/config/Role";

export type MenuType = {
  key: string;
  title: string;
  link?: string;
  icon?: string;
  role: string[];
};

export type MenuTypes = MenuType & {
  sub?: MenuType[];
};

const MENU: MenuTypes[] = [
  {
    key: "info",
    title: "Information",
    icon: "assignment",
    role: [Role.ADMIN, Role.USER],
    sub: [
      {
        key: "userInfo",
        title: "User Info",
        link: "/user-info",
        role: [Role.ADMIN, Role.USER],
      },
      {
        key: "allUser",
        title: "User List",
        link: "/user-list",
        role: [Role.ADMIN],
      },
    ],
  },
  {
    key: "package",
    title: "Package",
    icon: "card_travel",
    role: [Role.ADMIN, Role.USER],
    sub: [
      {
        key: "packagepriceList",
        role: [Role.ADMIN],
        title: "Delivery Type",
        link: "/package-pricelist",
      },
      {
        key: "packageList",
        role: [Role.ADMIN, Role.USER],
        title: "Package List",
        link: "/package-list",
      },
    ],
  },
  {
    key: "Search",
    role: [Role.ADMIN, Role.USER],
    title: "Search",
    icon: "search",
    link: "/package-search",
  },
];

export default MENU;
