import { Popconfirm, Space, Table } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import type { Tariff } from "@/domain";
import { useTariffListState } from "./TariffList.state";
import { useAuthUser } from "@/hooks/useAuthUser.ts";

export const TariffListView = ({
                                   tariffs,
                                   loading,
                                   navigate,
                                   deleteTariff
                               }: ReturnType<typeof useTariffListState>) => {

    const user = useAuthUser();
    const isManager = user?.roles?.includes("Manager");

    const actions = (_: unknown, tariff: Tariff) => (
        isManager ? (
            <Space size="middle">
                <EditOutlined
                    style={{ cursor: "pointer" }}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/tariffs/${tariff.id}/edit`);
                    }}
                />
                <Popconfirm
                    title="Удалить тариф?"
                    description="Действие нельзя отменить"
                    okText="Удалить"
                    cancelText="Отмена"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        deleteTariff(tariff.id);
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
        { title: "Название", dataIndex: "name", key: "name" },
        {
            title: "Стоимость в день",
            dataIndex: "dailyRate",
            key: "dailyRate",
            render: (value: number) => `${value} ₽`
        },
        {
            title: "",
            key: "actions",
            width: 80,
            render: actions
        }
    ];

    return (
        <Table
            rowKey="id"
            dataSource={[...tariffs].sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(tariff) => ({
                onClick: () => navigate(`/tariffs/${tariff.id}`),
                style: { cursor: "pointer" }
            })}
        />
    );
};
