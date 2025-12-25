import { Table, Space, Popconfirm } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";
import { useReaderCategoryListState } from './ReaderCategoryList.state.ts';
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const ReaderCategoryListView = ({
                                           categories,
                                           loading,
                                           navigate,
                                           deleteCategory
                                       }: ReturnType<typeof useReaderCategoryListState>) => {

    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    const actions = (_: unknown, category: ReaderCategory) => (
        isManager ? (
            <Space size="middle">
                <EditOutlined
                    style={{ cursor: 'pointer' }}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/reader-categories/${category.id}`);
                    }}
                />
                <Popconfirm
                    title="Удалить категорию?"
                    description="Действие нельзя отменить"
                    okText="Удалить"
                    cancelText="Отмена"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        deleteCategory(category.id);
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
        { title: 'Название', dataIndex: 'name', key: 'name' },
        { title: 'Скидка', dataIndex: 'discountRate', key: 'discountRate', render: (rate: number) => rate ? `${rate}%` : '-' },
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
            dataSource={categories.sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(category) => ({
                onClick: () => navigate(`/reader-categories/${category.id}`),
                style: { cursor: "pointer" }
            })}
        />
    );
};
