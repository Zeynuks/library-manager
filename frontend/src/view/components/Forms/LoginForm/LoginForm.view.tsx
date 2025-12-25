import type { useLoginFormState } from './LoginForm.state.ts';
import {Button, Card, Form, Input} from "antd";
import {LockOutlined, LoginOutlined, UserOutlined} from "@ant-design/icons";

export const LoginFormView = ({
                                  onFinish,
                                  disabled
                              }: ReturnType<typeof useLoginFormState>) => {
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
