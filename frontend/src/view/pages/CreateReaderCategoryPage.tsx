import {Col, Layout, Row} from "antd";
import {Content} from "antd/es/layout/layout";
import {Header} from "@/view/components/Header/Header.tsx";
import {ReaderCategoryForm} from "@/view/components/Forms/ReaderCategoryForm/ReaderCategoryForm.tsx";

export const CreateReaderCategoryPage = () => {
    return (
        <Layout style={{minHeight: "100vh"}}>
            <Header/>
            <Content>
                <Row justify="center" align="middle" style={{minHeight: "calc(100vh - 64px)"}}>
                    <Col>
                        <ReaderCategoryForm/>
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
