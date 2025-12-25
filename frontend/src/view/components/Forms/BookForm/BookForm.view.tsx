import {Button, Card, Form, Input, InputNumber, Row, Select} from "antd";
import { useBookFormState } from "@/view/components/Forms/BookForm/BookForm.state.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";
import * as React from "react";
import type { Book } from "@/domain";

export const BookFormView = ({
                                 book,
                                 tariffs,
                                 disabled,
                                 setDisabled,
                                 save
                             }: ReturnType<typeof useBookFormState>) => {

    const [form] = Form.useForm<Book>();
    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    React.useEffect(() => {
        form.setFieldsValue(book ?? {});
    }, [book]);

    const isCreateMode = !book;

    return (
        <Row>
            <Card title={isCreateMode ? 'Новая книга' : 'Книга'} style={{ width: 500 }}>
                <Form
                    form={form}
                    layout="vertical"
                    disabled={!isCreateMode && disabled}
                    onFinish={save}
                >
                    <Form.Item name="title" label="Название" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="author" label="Автор" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="genre" label="Жанр" rules={[{ required: true }]}>
                        <Input />
                    </Form.Item>

                    <Form.Item name="deposit" label="Залог" rules={[{ required: true }]}>
                        <InputNumber min={0} style={{ width: '100%' }} />
                    </Form.Item>

                    <Form.Item name="tariffId" label="Тариф" rules={[{ required: true }]}>
                        <Select
                            options={tariffs.map(t => ({
                                label: `${t.name} (${t.dailyRate}₽/день)`,
                                value: t.id
                            }))}
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
                            <Button block onClick={() => { form.setFieldsValue(book); setDisabled(true); }}>Отмена</Button>
                            <Button type="primary" block onClick={() => form.submit()}>Сохранить</Button>
                        </div>
                    )
                )}
            </Card>
        </Row>
    );
};
