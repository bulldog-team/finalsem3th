export interface IAuthReducer {
  username: string | null;
  email: string | null;
  acToken: string | null;
  rfToken: string | null;
  error: string | null;
  loading: boolean;
}
