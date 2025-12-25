import { TariffList } from "@/view/components/Lists/TariffList/TariffList";
import { Header } from "@/view/components/Header/Header";
import { Col, Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";

export const TariffListPage = () => (
    <Layout style={{ minHeight: "100vh" }}>
        <Header />
        <Content style={{ padding: "24px" }}>
            <Row justify="center">
                <Col span={24}>
                    <TariffList />
                </Col>
            </Row>
        </Content>
    </Layout>
);
