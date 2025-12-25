import { ReaderList } from "@/view/components/Lists/ReaderList/ReaderList.tsx";
import { Header } from "@/view/components/Header/Header.tsx";
import { Col, Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";

export const ReaderListPage = () => (
    <Layout style={{ minHeight: '100vh' }}>
        <Header />
        <Content style={{ padding: '24px' }}>
            <Row justify="center">
                <Col span={24}>
                    <ReaderList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
