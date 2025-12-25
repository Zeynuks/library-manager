import {BookForm} from "@/view/components/Forms/BookForm/BookForm";
import {Header} from "@/view/components/Header/Header.tsx";
import {Layout, Row} from "antd";
import {Content} from "antd/es/layout/layout";

export const CreateBookPage = () => {
    return (
        <Layout style={{minHeight: "100vh"}}>
            <Header/>
            <Content>
                <Row justify="center" align="middle" style={{minHeight: "calc(100vh - 64px)"}}>
                    <BookForm/>
                </Row>
            </Content>
        </Layout>
    );
};
