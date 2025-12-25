import * as React from "react";
import {Button, Card, Col, Form, Input, Row, Select} from "antd";
import {useAuthUser} from "@/hooks/useAuthUser.ts";
import type {User} from "@/domain/User.ts";
import {useUserFormState} from "./UserForm.state.ts";

type UserRole = 0 | 1 | 2;

const USER_ROLE_LABEL: Record<UserRole, string> = {
    0: "Administrator",
    1: "Manager",
    2: "Operator",
};

export const UserFormView = ({
                                 user,
                                 roles,
                                 disabled,
                                 blocked,
                                 setDisabled,
                                 toggleBlockUser,
                                 save,
                             }: ReturnType<typeof useUserFormState>) => {
    const [form] = Form.useForm<User>();
    const currentUser = useAuthUser();
    const isAdmin = currentUser?.roles?.includes("Administrator");
    const isCreateMode = !user;

    React.useEffect(() => {
        form.setFieldsValue(user ?? {});
    }, [user]);

    return (
        <Row>
            <Card title={isCreateMode ? "Новый пользователь" : "Пользователь"} style={{ width: 500 }}>
                {isAdmin && !isCreateMode && (
                    <Row justify="end">
                        <Button
                            type="primary"
                            danger={blocked}
                            onClick={toggleBlockUser}
                        >
                            {blocked ? "Заблокирован" : "Разблокирован"}
                        </Button>
                    </Row>
                )}
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item name="login" label="Логин" rules={[{required: true}]}>
                        <Input />
                    </Form.Item>

                    {isCreateMode && (
                        <Form.Item name="password" label="Пароль" rules={[{required: true}]}>
                            <Input.Password />
                        </Form.Item>
                    )}

                    <Form.Item
                        name={["roles", 0]}
                        label="Роль"
                        rules={[{ required: true }]}
                    >
                        <Select
                            options={roles.map((r) => ({
                                value: r,
                                label: USER_ROLE_LABEL[r as unknown as UserRole],
                            }))}
                        />
                    </Form.Item>
                </Form>
                {isAdmin && (
                    isCreateMode ? (
                        <Col style={{display: "flex", gap: 8}}>
                            <Button block onClick={() => form.resetFields()}>Очистить</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Создать</Button>
                        </Col>
                    ) : disabled ? (
                        <Button type="primary" block onClick={() => setDisabled(false)}>Редактировать</Button>
                    ) : (
                        <Col style={{display: "flex", gap: 8}}>
                            <Button block onClick={() => {
                                form.setFieldsValue(user);
                                setDisabled(true);
                            }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </Col>
                    )
                )}
            </Card>
        </Row>
    );
};
