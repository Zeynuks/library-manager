import { Button, Result } from "antd";
import { useNavigate } from "react-router-dom";

export const ForbiddenPage = () => {
    const navigate = useNavigate();

    return (
        <Result
            status="403"
            title="403"
            subTitle="Извините, у вас нет доступа к этой странице."
            extra={
                <Button type="primary" onClick={() => navigate("/")}>
                    На главную
                </Button>
            }
        />
    );
};
