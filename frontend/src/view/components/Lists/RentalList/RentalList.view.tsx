import {Popconfirm, Space, Table} from "antd";
import {DeleteOutlined, EditOutlined} from "@ant-design/icons";
import type {Rental} from "@/domain/Rental.ts";
import {useRentalListState} from './RentalList.state.ts';
import {useAuthUser} from "@/hooks/useAuthUser.ts";

export const RentalListView = ({
                                   rentals,
                                   loading,
                                   navigate,
                                   deleteRental
                               }: ReturnType<typeof useRentalListState>) => {

    const user = useAuthUser();
    const actions = (_: unknown, rental: Rental) => (
        user?.roles?.includes("Operator") ? (
            <Space size="middle">
                <EditOutlined
                    style={{cursor: 'pointer'}}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/rentals/${rental.id}`);
                    }}
                />
                {user?.roles?.includes("Manager") && (
                    <Popconfirm
                        title="Удалить аренду?"
                        description="Действие нельзя отменить"
                        okText="Удалить"
                        cancelText="Отмена"
                        onConfirm={(e) => {
                            e?.stopPropagation();
                            deleteRental(rental.id);
                        }}
                    >
                        <DeleteOutlined
                            onClick={(e) => e.stopPropagation()}
                            style={{cursor: 'pointer', color: 'red'}}
                        />
                    </Popconfirm>)}
            </Space>
        ) : null
    );

    const columns = [
        {
            title: 'Книга',
            key: 'book',
            render: (_: unknown, rental: Rental) => rental.book?.title ?? ''
        },
        {
            title: 'Читатель',
            key: 'reader',
            render: (_: unknown, rental: Rental) => rental.reader ? `${rental.reader.firstName} ${rental.reader.lastName}` : ''
        },
        {title: 'Дата выдачи', dataIndex: 'issueDate', key: 'issueDate'},
        {title: 'Ожидаемая дата возврата', dataIndex: 'expectedReturnDate', key: 'expectedReturnDate'},
        {title: 'Фактическая дата возврата', dataIndex: 'actualReturnDate', key: 'actualReturnDate'},
        {title: 'Сумма аренды', dataIndex: 'rentalAmount', key: 'rentalAmount'},
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
            dataSource={rentals.sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(rental) => ({
                onClick: () => navigate(`/rentals/${rental.id}`),
                style: {cursor: "pointer"}
            })}
        />
    );
};
