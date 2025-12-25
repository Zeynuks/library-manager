import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Col, Layout, Row, Spin } from "antd";
import { Content } from "antd/es/layout/layout";
import { Header } from "@/view/components/Header/Header.tsx";
import { ReaderForm } from "@/view/components/Forms/ReaderForm/ReaderForm.tsx";
import { fetchReader } from "@/api";
import type { Reader } from "@/domain/Reader.ts";

export const ReaderPage = () => {
    const [loading, setLoading] = useState(true);
    const [reader, setReader] = useState<Reader>();
    const { id } = useParams<{ id: string }>();
    const readerId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setReader(await fetchReader(readerId));
            setLoading(false);
        };
        loadData();
    }, [readerId]);

    if (!id) return null;
    if (loading || !reader) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row justify="center" align="middle" style={{ minHeight: "calc(100vh - 64px)" }}>
                    <Col>
                        <ReaderForm reader={reader} />
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
