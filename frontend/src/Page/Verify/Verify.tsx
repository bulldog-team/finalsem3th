import { Button, Result } from "antd";
import { useTranslation } from "react-i18next";

const VerifyPage = () => {
  const { t } = useTranslation();
  return (
    <Result
      status="success"
      title={t("Verify.confirm")}
      extra={<Button type="primary">{t("Verify.return")}</Button>}
    />
  );
};

export default VerifyPage;
