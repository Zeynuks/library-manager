import { Popconfirm, Space, Table } from "antd";
import { useBookListState } from './BookList.state.ts';
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useAuthUser } from "@/hooks/useAuthUser.ts";
import type { Book } from "@/domain";

export const BookListView = ({
                                 books,
                                 loading,
                                 navigate,
                                 deleteBook
                             }: ReturnType<typeof useBookListState>) => {

    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    const actions = (_: unknown, book: Book) => (
        isManager ? (
            <Space size="middle">
                <EditOutlined
                    style={{ cursor: 'pointer' }}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/books/${book.id}`);
                    }}
                />
                <Popconfirm
                    title="Удалить книгу?"
                    description="Действие нельзя отменить"
                    okText="Удалить"
                    cancelText="Отмена"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        deleteBook(book.id);
                    }}
                >
                    <DeleteOutlined
                        onClick={(e) => e.stopPropagation()}
                        style={{ cursor: 'pointer', color: 'red' }}
                    />
                </Popconfirm>
            </Space>
        ) : null
    );

    const columns = [
        { title: 'Название', dataIndex: 'title', key: 'title' },
        { title: 'Автор', dataIndex: 'author', key: 'author' },
        { title: 'Жанр', dataIndex: 'genre', key: 'genre' },
        { title: 'Залог', dataIndex: 'deposit', key: 'deposit' },
        {
            title: 'Тариф',
            dataIndex: 'tariff',
            key: 'tariff',
            render: (_: unknown, book: Book) => book.tariff ? `${book.tariff.name} (${book.tariff.dailyRate}₽/день)` : ''
        },
        {
            title: '',
            key: 'actions',
            width: 80,
            render: actions
        }
    ];

    return (
        <Table
            rowKey="id"
            dataSource={books.sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(book) => ({
                onClick: () => navigate(`/books/${book.id}`),
                style: { cursor: "pointer" }
            })}
        />
    );
};
