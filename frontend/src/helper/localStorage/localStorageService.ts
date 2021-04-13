export interface IUserData {
  username: string | null;
  email: string | null;
  token: string | null;
  role: string[] | undefined;
  userId: number | null;
}

export interface ILocalStorageService {
  setUserData: (userData: IUserData) => void;
  getUserData: () => IUserData;
  getUsername: () => string | null;
  getUserId: () => number | null;
  getToken: () => string | null;
  getEmail: () => string | null;
  getRole: () => string[] | undefined;
  clearAll: () => void;
}

export const localStorageService: ILocalStorageService = {
  setUserData: (userData: IUserData) => {
    if (userData.username) {
      localStorage.setItem("username", userData.username);
    }
    if (userData.email) {
      localStorage.setItem("email", userData.email);
    }
    if (userData.token) {
      localStorage.setItem("token", userData.token);
    }
    if (userData.userId) {
      localStorage.setItem("userId", userData.userId.toString());
    }
    if (userData.role) {
      localStorage.setItem("role", userData.role.toString());
    }
  },

  getUserData: () => {
    return {
      token: localStorageService.getToken(),
      email: localStorageService.getEmail(),
      role: localStorageService.getRole(),
      username: localStorageService.getUsername(),
      userId: localStorageService.getUserId(),
    };
  },

  getUsername: () => localStorage.getItem("username"),

  getUserId: () => {
    const temp = localStorage.getItem("userId");
    if (temp) {
      return parseInt(temp);
    }
    return null;
  },

  getToken: () => localStorage.getItem("token"),

  getEmail: () => localStorage.getItem("email"),

  getRole: () => {
    const temp = localStorage.getItem("role");
    const role = temp ? temp.split(",") : undefined;
    return role;
  },

  clearAll: () => localStorage.clear(),
};
