import {Profile} from "../components/Profile/Profile";
import {Header} from "@/view/components/Header/Header.tsx";
import {Col, Layout, Row} from "antd";
import {Content} from "antd/es/layout/layout";

export const ProfilePage = () => (
    <Layout  style={{ minHeight: '100vh' }}>
        <Header/>
        <Content>
            <Row
                justify="center"
                align="middle"
                style={{ minHeight: 'calc(100vh - 64px)' }}
            >
                <Col>
                    <Profile/>
                </Col>
            </Row>
        </Content>
    </Layout>
);
