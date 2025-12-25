import { Button, Card, Form, Input, InputNumber, Row } from "antd";
import * as React from "react";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";
import { useReaderCategoryFormState } from "./ReaderCategoryForm.state.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const ReaderCategoryFormView = ({
                                           category,
                                           disabled,
                                           setDisabled,
                                           save
                                       }: ReturnType<typeof useReaderCategoryFormState>) => {
    const [form] = Form.useForm<ReaderCategory>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    React.useEffect(() => {
        form.setFieldsValue(category ?? {});
    }, [category]);

    const isCreateMode = !category;

    return (
        <Row>
            <Card title={isCreateMode ? "Новая категория" : "Категория"} style={{ width: 500 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item name="name" label="Название" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="discountRate" label="Скидка (%)">
                        <InputNumber min={0} max={100} style={{ width: "100%" }} />
                    </Form.Item>
                </Form>

                {isManager && (
                    isCreateMode ? (
                        <div style={{ display: "flex", gap: 8 }}>
                            <Button block onClick={() => form.resetFields()}>Очистить</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Создать</Button>
                        </div>
                    ) : disabled ? (
                        <Button type="primary" block onClick={() => setDisabled(false)}>Редактировать</Button>
                    ) : (
                        <div style={{ display: "flex", gap: 8 }}>
                            <Button block onClick={() => { form.setFieldsValue(category); setDisabled(true); }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
