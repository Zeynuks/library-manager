import { Header } from "@/view/components/Header/Header.tsx";
import { Col, Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";
import { RentalList } from "@/view/components/Lists/RentalList/RentalList.tsx";

export const RentalListPage = () => (
    <Layout style={{ minHeight: '100vh' }}>
        <Header />
        <Content style={{ padding: '24px' }}>
            <Row justify="center">
                <Col span={24}>
                    <RentalList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
