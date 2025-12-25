import {useMemo} from "react";
import {Button, Dropdown, Layout, Space, Typography} from "antd";
import {
    BookOutlined,
    DollarOutlined,
    LoginOutlined,
    LogoutOutlined,
    MenuOutlined,
    TagsOutlined,
    UserOutlined
} from "@ant-design/icons";
import type {useHeaderState} from "@/view/components/Header/Header.state.ts";

const {Header} = Layout;
const {Text} = Typography;

export const HeaderView = ({user, onLogout, navigate}: ReturnType<typeof useHeaderState>) => {

    const menuItems = useMemo(() => {
        if (!user?.roles) return [];

        return [
            {
                key: "books",
                icon: <BookOutlined/>,
                label: "Книги",
                children: user.roles.some((role) => role === "Manager")
                    ? [
                        {key: "books:create", label: "Создать книгу", onClick: () => navigate("/books/create")},
                        {key: "books:list", label: "Список книг", onClick: () => navigate("/books")},
                    ]
                    : [
                        {key: "books:list", label: "Список книг", onClick: () => navigate("/books")},
                    ],
            },
            {
                key: "tariffs",
                icon: <DollarOutlined/>,
                label: "Тарифы",
                children: user.roles.some((role) => role === "Manager")
                    ? [
                        {key: "tariffs:create", label: "Создать тариф", onClick: () => navigate("/tariffs/create")},
                        {key: "tariffs:list", label: "Список тарифов", onClick: () => navigate("/tariffs")},
                    ]
                    : [
                        {key: "tariffs:list", label: "Список тарифов", onClick: () => navigate("/tariffs")},
                    ],
            },
            {
                key: "fines",
                icon: <DollarOutlined/>,
                label: "Штрафы",
                children: user.roles.some((role) => role === "Manager")
                    ? [
                        {key: "fines:create", label: "Создать штраф", onClick: () => navigate("/fines/create")},
                        {key: "fines:list", label: "Список штрафов", onClick: () => navigate("/fines")},
                    ]
                    : [
                        {key: "fines:list", label: "Список штрафов", onClick: () => navigate("/fines")},
                    ],
            },
            {
                key: "categories",
                icon: <TagsOutlined/>,
                label: "Категории читателей",
                children: user.roles.some((role) => role === "Manager")
                    ? [
                        {
                            key: "categories:create",
                            label: "Создать категорию",
                            onClick: () => navigate("/reader-categories/create")
                        },
                        {
                            key: "categories:list",
                            label: "Список категорий",
                            onClick: () => navigate("/reader-categories")
                        },
                    ]
                    : [
                        {
                            key: "categories:list",
                            label: "Список категорий",
                            onClick: () => navigate("/reader-categories")
                        },
                    ],
            },
            {
                key: "readers",
                icon: <UserOutlined/>,
                label: "Читатели",
                children: user.roles.some((role) => role === "Manager")
                    ? [
                        {key: "readers:create", label: "Создать читателя", onClick: () => navigate("/readers/create")},
                        {key: "readers:list", label: "Список читателей", onClick: () => navigate("/readers")},
                    ]
                    : [
                        {key: "readers:list", label: "Список читателей", onClick: () => navigate("/readers")},
                    ],
            },
            {
                key: "rentals",
                icon: <BookOutlined/>,
                label: "Аренды",
                children: user.roles.some((role) => role === "Operator")
                    ? [
                        {key: "rentals:create", label: "Создать аренду", onClick: () => navigate("/rentals/create")},
                        {key: "rentals:list", label: "Список аренд", onClick: () => navigate("/rentals")},
                    ]
                    : [{key: "rentals:list", label: "Список аренд", onClick: () => navigate("/rentals")}],
            },
        ];
    }, [user, navigate]);

    return (
        <Header
            style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                background: "#fff",
                padding: "0 24px",
                borderBottom: "1px solid #f0f0f0"
            }}
        >
            <Text strong style={{fontSize: 16}}>LibraryManager</Text>

            <Space>
                {user && menuItems.length > 0 && (
                    <Dropdown menu={{items: menuItems}} placement="bottomRight" trigger={["click"]}>
                        <Button icon={<MenuOutlined/>}>Меню</Button>
                    </Dropdown>
                )}

                {user ? (
                    <Space>
                        <Text><UserOutlined/> {user.login}</Text>
                        <Button danger icon={<LogoutOutlined/>} onClick={onLogout}>Выйти</Button>
                    </Space>
                ) : (
                    <Button type="primary" icon={<LoginOutlined/>} onClick={() => navigate("/login")}>Войти</Button>
                )}
            </Space>
        </Header>
    );
};
