import { Header } from "@/view/components/Header/Header.tsx";
import { Col, Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";
import {FineList} from "@/view/components/Lists/FineList/FineList.tsx";

export const FineListPage = () => (
    <Layout style={{ minHeight: '100vh' }}>
        <Header />
        <Content style={{ padding: '24px' }}>
            <Row justify="center">
                <Col span={24}>
                    <FineList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
