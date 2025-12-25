import { BookList } from "@/view/components/Lists/BookList/BookList.tsx";
import { Header } from "@/view/components/Header/Header.tsx";
import { Col, Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";

export const BookListPage = () => (
    <Layout style={{ minHeight: '100vh' }}>
        <Header />
        <Content style={{ padding: '24px' }}>
            <Row justify="center">
                <Col span={24}>
                    <BookList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
