import { Button, Card, DatePicker, Form, InputNumber } from "antd";
import dayjs from "dayjs";
import { useRentalReturnFormState } from "./RentalReturnForm.state.ts";

export const RentalReturnFormView = ({ submit, loading, totalAmount }: ReturnType<typeof useRentalReturnFormState>) => {
    const [form] = Form.useForm();

    return (
        <Card title="Возврат книги" style={{ width: 400 }}>
            <Form
                form={form}
                layout="vertical"
                onFinish={(values) => submit(values.actualReturnDate)}
                initialValues={{
                    actualReturnDate: dayjs(),
                }}
            >
                <Form.Item
                    name="actualReturnDate"
                    label="Фактическая дата возврата"
                    rules={[{ required: true }]}
                >
                    <DatePicker style={{ width: "100%" }} />
                </Form.Item>

                {totalAmount !== null && (
                    <Form.Item label="Итоговая сумма">
                        <InputNumber
                            value={totalAmount}
                            disabled
                            style={{ width: "100%" }}
                        />
                    </Form.Item>
                )}

                {totalAmount == null && (
                    <Button
                        type="primary"
                        htmlType="submit"
                        block
                        loading={loading}
                    >
                        Вернуть книгу
                    </Button>
                )}
            </Form>
        </Card>
    );
};
