import { Popconfirm, Space, Table } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import type { Reader } from "@/domain/Reader.ts";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const ReaderListView = ({
                                   readers,
                                   loading,
                                   navigate,
                                   deleteReader
                               }: ReturnType<typeof import('./ReaderList.state.ts').useReaderListState>) => {

    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    const actions = (_: unknown, reader: Reader) => (
        isManager ? (
            <Space size="middle">
                <EditOutlined
                    style={{ cursor: "pointer" }}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/readers/${reader.id}`);
                    }}
                />
                <Popconfirm
                    title="Удалить читателя?"
                    description="Действие нельзя отменить"
                    okText="Удалить"
                    cancelText="Отмена"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        deleteReader(reader.id);
                    }}
                >
                    <DeleteOutlined
                        onClick={(e) => e.stopPropagation()}
                        style={{ cursor: "pointer", color: "red" }}
                    />
                </Popconfirm>
            </Space>
        ) : null
    );

    const columns = [
        { title: "Фамилия", dataIndex: "lastName", key: "lastName" },
        { title: "Имя", dataIndex: "firstName", key: "firstName" },
        { title: "Отчество", dataIndex: "middleName", key: "middleName" },
        { title: "Адрес", dataIndex: "address", key: "address" },
        { title: "Телефон", dataIndex: "phoneNumber", key: "phoneNumber" },
        {
            title: "Категория",
            dataIndex: "category",
            key: "category",
            render: (_: unknown, reader: Reader) => reader.category?.name ?? ""
        },
        { title: "", key: "actions", width: 80, render: actions }
    ];

    return (
        <Table
            rowKey="id"
            dataSource={readers.sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(reader) => ({
                onClick: () => navigate(`/readers/${reader.id}`),
                style: { cursor: "pointer" }
            })}
        />
    );
};
