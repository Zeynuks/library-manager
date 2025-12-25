import { Popconfirm, Space, Table } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import type { Fine } from "@/domain/Fine.ts";
import { useFineListState } from './FineList.state.ts';
import {useAuthUser} from "@/hooks/useAuthUser.ts";

export const FineListView = ({
                                 fines,
                                 loading,
                                 navigate,
                                 deleteFine
                             }: ReturnType<typeof useFineListState>) => {

    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    const actions = (_: unknown, fine: Fine) => (
        isManager ? (
            <Space size="middle">
                <EditOutlined
                    style={{ cursor: 'pointer' }}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/fines/${fine.id}`);
                    }}
                />
                <Popconfirm
                    title="Удалить штраф?"
                    description="Действие нельзя отменить"
                    okText="Удалить"
                    cancelText="Отмена"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        deleteFine(fine.id);
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
        { title: 'Описание', dataIndex: 'description', key: 'description' },
        { title: 'Сумма', dataIndex: 'amount', key: 'amount' },
        {
            title: 'Читатель',
            dataIndex: 'readerId',
            key: 'reader',
            render: (_: unknown, fine: Fine) => `${fine.rental?.reader?.firstName || ''} ${fine.rental?.reader?.middleName || ''} ${fine.rental?.reader?.lastName || ''}`.trim()
        },
        {
            title: 'Книга',
            dataIndex: 'bookId',
            key: 'book',
            render: (_: unknown, fine: Fine) => fine.rental?.book?.title || ''
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
            dataSource={fines.sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(fine) => ({
                onClick: () => navigate(`/fines/${fine.id}`),
                style: { cursor: "pointer" }
            })}
        />
    );
};
