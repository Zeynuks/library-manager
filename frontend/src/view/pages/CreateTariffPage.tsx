import { TariffForm } from "@/view/components/Forms/TariffForm/TariffForm";
import { Header } from "@/view/components/Header/Header";
import { Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";

export const CreateTariffPage = () => (
    <Layout style={{ minHeight: "100vh" }}>
        <Header />
        <Content>
            <Row
                justify="center"
                align="middle"
                style={{ minHeight: "calc(100vh - 64px)" }}
            >
                <TariffForm />
            </Row>
        </Content>
    </Layout>
);
