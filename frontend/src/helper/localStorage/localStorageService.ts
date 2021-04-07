export interface IUserData {
  username: string | null;
  email: string | null;
  acToken: string | null;
  rfToken: string | null;
  role: string[] | null;
}

export interface ILocalStorageService {
  setUserData: (userData: IUserData) => void;
  getUserData: () => IUserData;
  getUsername: () => string | null;
  getAcToken: () => string | null;
  getRfToken: () => string | null;
  getEmail: () => string | null;
  getRole: () => string[] | null;
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
    if (userData.rfToken) {
      localStorage.setItem("rfToken", userData.rfToken);
    }
    if (userData.acToken) {
      localStorage.setItem("acToken", userData.acToken);
    }
  },

  getUserData: () => {
    return {
      acToken: localStorageService.getAcToken(),
      email: localStorageService.getEmail(),
      rfToken: localStorageService.getRfToken(),
      role: localStorageService.getRole(),
      username: localStorageService.getUsername(),
    };
  },

  getUsername: () => localStorage.getItem("username"),

  getAcToken: () => localStorage.getItem("acToken"),

  getRfToken: () => localStorage.getItem("rfToken"),

  getEmail: () => localStorage.getItem("email"),

  getRole: () => {
    const temp = localStorage.getItem("role");
    const role = temp ? JSON.parse(temp) : null;
    return role;
  },

  clearAll: () => localStorage.clear(),
};
