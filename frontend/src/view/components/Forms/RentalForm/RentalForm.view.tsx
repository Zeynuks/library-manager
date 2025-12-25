import { Button, Card, DatePicker, Form, InputNumber, Row, Select } from "antd";
import * as React from "react";
import type { Rental } from "@/domain/Rental.ts";
import { useRentalFormState } from "./RentalForm.state.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";
import dayjs from "dayjs";

export const RentalFormView = ({
                                   rental,
                                   books,
                                   readers,
                                   disabled,
                                   setDisabled,
                                   save
                               }: ReturnType<typeof useRentalFormState>) => {

    const [form] = Form.useForm<Rental>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Operator");

    React.useEffect(() => {
        form.setFieldsValue(rental ?? {});
    }, [rental]);

    const isCreateMode = !rental?.id;

    React.useEffect(() => {
        if (rental) {
            form.setFieldsValue({
                ...rental,
                issueDate: rental.issueDate ? dayjs(rental.issueDate) : undefined,
                expectedReturnDate: rental.expectedReturnDate ? dayjs(rental.expectedReturnDate) : undefined,
                actualReturnDate: rental.actualReturnDate ? dayjs(rental.actualReturnDate) : undefined,
            });
        }
    }, [rental]);

    const handleSave = async (values: Rental) => {
        if (typeof values.issueDate !== "string" && typeof values.expectedReturnDate !== "string" && typeof values.actualReturnDate !== "string") {
            const payload = {
                ...values,
                issueDate: values.issueDate?.toISOString().split('T')[0],
                expectedReturnDate: values.expectedReturnDate?.toISOString().split('T')[0],
            };
            await save(payload);
        }
    };

    return (
        <Row>
            <Card title={isCreateMode ? 'Новая аренда' : 'Аренда'} style={{ width: 500 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={handleSave}
                >
                    <Form.Item name="bookId" label="Книга" rules={[{ required: true }]}>
                        <Select options={books.map(b => ({ label: b.title, value: b.id }))} />
                    </Form.Item>

                    <Form.Item name="readerId" label="Читатель" rules={[{ required: true }]}>
                        <Select options={readers.map(r => ({ label: `${r.firstName} ${r.lastName}`, value: r.id }))} />
                    </Form.Item>

                    <Form.Item name="issueDate" label="Дата выдачи" rules={[{ required: true }]}>
                        <DatePicker style={{ width: '100%' }} />
                    </Form.Item>

                    <Form.Item name="expectedReturnDate" label="Ожидаемая дата возврата" rules={[{ required: true }]}>
                        <DatePicker style={{ width: '100%' }} />
                    </Form.Item>

                    {rental?.rentalAmount != null && (
                        <Form.Item label="Сумма аренды">
                            <InputNumber value={rental.rentalAmount} disabled style={{ width: '100%' }} />
                        </Form.Item>
                    )}
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
                            <Button block onClick={() => {
                                form.setFieldsValue({
                                    ...rental,
                                    issueDate: rental.issueDate ? dayjs(rental.issueDate) : undefined,
                                    expectedReturnDate: rental.expectedReturnDate ? dayjs(rental.expectedReturnDate) : undefined,
                                    actualReturnDate: rental.actualReturnDate ? dayjs(rental.actualReturnDate) : undefined,
                                });
                                setDisabled(true);
                            }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
