import { Layout, Row, Col } from "antd";
import { UserList } from "@/view/components/Lists/UserList/UserList.tsx";
import { Header } from "@/view/components/Header/Header.tsx";
import { Content } from "antd/es/layout/layout";

export const UserListPage = () => (
    <Layout style={{ minHeight: "100vh" }}>
        <Header />
        <Content style={{ padding: "24px" }}>
            <Row justify="center">
                <Col span={24}>
                    <UserList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
