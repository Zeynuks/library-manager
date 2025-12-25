import { Button, Card, Form, Input, InputNumber, Row } from "antd";
import type { Tariff } from "@/domain/Tariff";
import * as React from "react";
import { useTariffFormState } from "./TariffForm.state";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const TariffFormView = ({
                                   tariff,
                                   disabled,
                                   setDisabled,
                                   save
                               }: ReturnType<typeof useTariffFormState>) => {

    const [form] = Form.useForm<Tariff>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    React.useEffect(() => {
        form.setFieldsValue(tariff ?? {});
    }, [tariff]);

    const isCreateMode = !tariff;

    return (
        <Row>
            <Card title={isCreateMode ? "Новый тариф" : "Тариф"} style={{ width: 400 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item
                        name="name"
                        label="Название"
                        rules={[{ required: true }]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item
                        name="dailyRate"
                        label="Стоимость в день"
                        rules={[{ required: true }]}
                    >
                        <InputNumber min={0} style={{ width: "100%" }} />
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
                            <Button block onClick={() => { form.setFieldsValue(tariff); setDisabled(true); }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
