import { Button, Card, Form, Input, InputNumber, Row, Select, Typography } from "antd";
import * as React from "react";
import type { Fine } from "@/domain/Fine.ts";
import { useFineFormState } from "./FineForm.state.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const FineFormView = ({
                                 fine,
                                 rental,
                                 rentals,
                                 disabled,
                                 setDisabled,
                                 save
                             }: ReturnType<typeof useFineFormState>) => {
    const [form] = Form.useForm<Fine>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    React.useEffect(() => {
        form.setFieldsValue(fine ?? {});
    }, [fine]);

    const isCreateMode = !fine;
    const bookTitle = rental?.book?.title ?? "-";
    const readerName = `${rental?.reader?.firstName} ${rental?.reader?.middleName} ${rental?.reader?.lastName}`.trim();

    return (
        <Row>
            <Card title={isCreateMode ? "Новый штраф" : "Штраф"} style={{ width: 500 }}>
                {!isCreateMode && (
                    <Card type="inner" title="Информация" style={{ marginBottom: 16 }}>
                        <Typography.Text strong>Книга:</Typography.Text> {bookTitle} <br />
                        <Typography.Text strong>Читатель:</Typography.Text> {readerName}
                    </Card>
                )}

                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    {isCreateMode && (
                        <Form.Item name="rentalId" label="Аренда" rules={[{ required: true }]}>
                            <Select
                                options={rentals.map(r => ({
                                    label: `Читатель: ${`${r.reader?.firstName || ''} ${r.reader?.middleName || ''} ${r.reader?.lastName || ''}`.trim()}. Книга: ${r.book?.title || 'Нет названия'}`,
                                    value: r.id
                                }))}
                            />
                        </Form.Item>
                    )}

                    <Form.Item name="description" label="Описание" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="amount" label="Сумма" rules={[{ required: true }]}>
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
                            <Button block onClick={() => { form.setFieldsValue(fine); setDisabled(true); }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
