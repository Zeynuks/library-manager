import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Col, Layout, Row, Spin } from "antd";
import { Content } from "antd/es/layout/layout";
import { Header } from "@/view/components/Header/Header.tsx";
import { UserForm } from "@/view/components/Forms/UserForm/UserForm";
import { fetchUser } from "@/api";
import type { User } from "@/domain/User.ts";

export const UserPage = () => {
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState<User>();
    const { id } = useParams<{ id: string }>();
    const userId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setUser(await fetchUser(userId));
            setLoading(false);
        };
        loadData();
    }, [userId]);

    if (!id) return null;
    if (loading || !user) return <Spin size="large" style={{ display: "block", margin: "100px auto" }} />;

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row justify="center" align="middle" style={{ minHeight: "calc(100vh - 64px)" }}>
                    <Col>
                        <UserForm user={user} />
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
