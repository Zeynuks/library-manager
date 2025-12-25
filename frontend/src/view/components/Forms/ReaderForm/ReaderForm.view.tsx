import { Button, Card, Form, Input, Row, Select } from "antd";
import * as React from "react";
import type { Reader } from "@/domain/Reader.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const ReaderFormView = ({
                                   reader,
                                   categories,
                                   disabled,
                                   setDisabled,
                                   save
                               }: ReturnType<typeof import('./ReaderForm.state.ts').useReaderFormState>) => {

    const [form] = Form.useForm<Reader>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    React.useEffect(() => {
        form.setFieldsValue(reader ?? {});
    }, [reader]);

    const isCreateMode = !reader;

    return (
        <Row>
            <Card title={isCreateMode ? 'Новый читатель' : 'Читатель'} style={{ width: 500 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item name="lastName" label="Фамилия" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="firstName" label="Имя" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="middleName" label="Отчество">
                        <Input />
                    </Form.Item>

                    <Form.Item name="address" label="Адрес">
                        <Input />
                    </Form.Item>

                    <Form.Item name="phoneNumber" label="Телефон">
                        <Input />
                    </Form.Item>

                    <Form.Item name="categoryId" label="Категория">
                        <Select
                            options={categories.map(c => ({ label: c.name, value: c.id }))}
                        />
                    </Form.Item>
                </Form>

                {isManager && (
                    isCreateMode ? (
                        <div style={{ display: 'flex', gap: 8 }}>
                            <Button block onClick={() => form.resetFields()}>Очистить</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Создать</Button>
                        </div>
                    ) : disabled ? (
                        <Button type="primary" block onClick={() => setDisabled(false)}>Редактировать</Button>
                    ) : (
                        <div style={{ display: 'flex', gap: 8 }}>
                            <Button block onClick={() => { form.setFieldsValue(reader); setDisabled(true); }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
