import {Popconfirm, Space, Table, Tag} from "antd";
import {DeleteOutlined, EditOutlined,} from "@ant-design/icons";
import {useAuthUser} from "@/hooks/useAuthUser";
import type {User} from "@/domain/User";
import {useUserListState} from "./UserList.state";
import type {UserRole} from "@/domain/UserRoles.ts";

export const UserListView = ({
                                 users,
                                 loading,
                                 navigate,
                                 removeUser,
                             }: ReturnType<typeof useUserListState>) => {
    const currentUser = useAuthUser();

    const isAdmin = currentUser?.roles?.includes("Administrator") ?? false;

    const actions = (_: unknown, user: User) =>
        isAdmin ? (
            <Space size="middle">
                <EditOutlined
                    style={{cursor: "pointer"}}
                    onClick={(e) => {
                        e.stopPropagation();
                        navigate(`/users/${user.id}`);
                    }}
                />

                <Popconfirm
                    title="Удалить пользователя?"
                    onConfirm={(e) => {
                        e?.stopPropagation();
                        removeUser(user.id);
                    }}
                >
                    <DeleteOutlined
                        style={{cursor: "pointer", color: "red"}}
                    />
                </Popconfirm>
            </Space>
        ) : null;

    const columns = [
        {
            title: "Логин",
            dataIndex: "login",
            key: "login",
        },
        {
            title: "Роли",
            dataIndex: "roles",
            key: "roles",
            render: (roles: UserRole[] = []) => (
                <>
                    {roles.map((role) => (
                        <Tag key={role}>{role}</Tag>
                    ))}
                </>
            ),
        },
        {
            title: "Статус",
            dataIndex: "isBlocked",
            key: "isBlocked",
            render: (blocked: boolean) =>
                blocked ? "Заблокирован" : "Разблокирован",
        },
        {
            title: "",
            key: "actions",
            width: 120,
            render: actions,
        },
    ];

    return (
        <Table
            rowKey="id"
            dataSource={[...users].sort((a, b) => a.id - b.id)}
            columns={columns}
            loading={loading}
            onRow={(user) => ({
                onClick: () => navigate(`/users/${user.id}`),
                style: {cursor: "pointer"},
            })}
        />
    );
};
