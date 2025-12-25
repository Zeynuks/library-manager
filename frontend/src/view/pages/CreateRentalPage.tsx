import { RentalForm } from "@/view/components/Forms/RentalForm/RentalForm";
import { Header } from "@/view/components/Header/Header.tsx";
import { Layout, Row } from "antd";
import { Content } from "antd/es/layout/layout";

export const CreateRentalPage = () => {
    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row justify="center" align="middle" style={{ minHeight: "calc(100vh - 64px)" }}>
                    <RentalForm />
                </Row>
            </Content>
        </Layout>
    );
};
