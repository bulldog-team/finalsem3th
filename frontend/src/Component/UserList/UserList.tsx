import { DatePicker } from "antd";
import moment, { Moment } from "moment";
import { useEffect, useState } from "react";

const UserList = () => {
  const [date, setDate] = useState<string>();
  const dateFormat = "YYYY-MM-DD";
  useEffect(() => {
    setDate(() => "1963-10-14T00:00:00");
  }, []);

  const onChange = (date: Moment | null, dateString: string) => {
    console.log(date, dateString);
  };
  return (
    <>
      User List
      <DatePicker defaultValue={moment(date, dateFormat)} />
    </>
  );
};

export default UserList;
