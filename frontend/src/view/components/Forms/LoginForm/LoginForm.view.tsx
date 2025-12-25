import type { useLoginFormState } from './LoginForm.state.ts';
import {Button, Card, Form, Input} from "antd";
import {LockOutlined, LoginOutlined, UserOutlined} from "@ant-design/icons";
import {useAuthUser} from "@/hooks/useAuthUser.ts";
import {useNavigate} from "react-router-dom";

export const LoginFormView = ({
                                  onFinish,
                                  disabled
                              }: ReturnType<typeof useLoginFormState>) => {
    const user = useAuthUser()
    const navigate = useNavigate();
    if (user.login !== undefined) {
        navigate("/profile");
    }

    return (
        <Card title="Вход" style={{ maxWidth: 400 }}>
            <Form onFinish={onFinish} layout="vertical" disabled={disabled}>
                <Form.Item
                    name="login"
                    label="Логин"
                    rules={[{ required: true, message: 'Введите логин' }]}
                >
                    <Input prefix={<UserOutlined />} />
                </Form.Item>

                <Form.Item
                    name="password"
                    label="Пароль"
                    rules={[{ required: true, message: 'Введите пароль' }]}
                >
                    <Input.Password prefix={<LockOutlined />} />
                </Form.Item>

                <Button
                    type="primary"
                    htmlType="submit"
                    icon={<LoginOutlined />}
                    block
                >
                    Войти
                </Button>
            </Form>
        </Card>
    );
};
