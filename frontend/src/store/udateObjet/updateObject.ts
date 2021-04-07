import { IAuthReducer } from "../reducer/reducerType";

type CurrentReducerObject = IAuthReducer;

const updateObject = (
  oldObject: CurrentReducerObject,
  updatedObject: object
): CurrentReducerObject => {
  return {
    ...oldObject,
    ...updatedObject,
  };
};

export default updateObject;
