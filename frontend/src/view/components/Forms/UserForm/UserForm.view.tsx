import { Button, Card, Form, Input, Row, Select, Switch } from "antd";
import * as React from "react";
import type { User } from "@/domain";
import { useUserFormState } from "./UserForm.state.ts";

const roleOptions = [
    { label: "Администратор", value: "Administrator" },
    { label: "Пользователь", value: "User" }
];

export const UserFormView = ({
                                 user,
                                 disabled,
                                 setDisabled,
                                 save
                             }: ReturnType<typeof useUserFormState>) => {

    const [form] = Form.useForm<User>();

    React.useEffect(() => {
        form.setFieldsValue(user ?? {});
    }, [user]);

    const isCreateMode = !user;

    return (
        <Row>
            <Card title={isCreateMode ? "Новый пользователь" : "Пользователь"} style={{ width: 500 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item
                        name="login"
                        label="Логин"
                        rules={[{ required: true }]}
                    >
                        <Input />
                    </Form.Item>

                    {isCreateMode && (
                        <Form.Item
                            name="password"
                            label="Пароль"
                            rules={[{ required: true }]}
                        >
                            <Input.Password />
                        </Form.Item>
                    )}

                    <Form.Item
                        name="role"
                        label="Роль"
                        rules={[{ required: true }]}
                    >
                        <Select options={roleOptions} />
                    </Form.Item>

                    {!isCreateMode && (
                        <Form.Item
                            name="isBlocked"
                            label="Заблокирован"
                            valuePropName="checked"
                        >
                            <Switch />
                        </Form.Item>
                    )}
                </Form>

                {isCreateMode ? (
                    <div style={{ display: "flex", gap: 8 }}>
                        <Button block onClick={() => form.resetFields()}>
                            Очистить
                        </Button>
                        <Button type="primary" block onClick={() => form.submit()}>
                            Создать
                        </Button>
                    </div>
                ) : disabled ? (
                    <Button
                        type="primary"
                        block
                        onClick={() => setDisabled(false)}
                    >
                        Редактировать
                    </Button>
                ) : (
                    <div style={{ display: "flex", gap: 8 }}>
                        <Button
                            block
                            onClick={() => {
                                form.setFieldsValue(user);
                                setDisabled(true);
                            }}
                        >
                            Отмена
                        </Button>
                        <Button type="primary" block onClick={() => form.submit()}>
                            Сохранить
                        </Button>
                    </div>
                )}
            </Card>
        </Row>
    );
};
