import { Header } from "@/view/components/Header/Header.tsx";
import { Layout, Row, Col } from "antd";
import { Content } from "antd/es/layout/layout";
import { ReaderCategoryList } from "@/view/components/Lists/ReaderCategoryList/ReaderCategoryList.tsx";

export const ReaderCategoryListPage = () => (
    <Layout style={{ minHeight: '100vh' }}>
        <Header />
        <Content style={{ padding: '24px' }}>
            <Row justify="center">
                <Col span={24}>
                    <ReaderCategoryList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
