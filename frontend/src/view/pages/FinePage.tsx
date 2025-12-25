import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Col, Layout, Row, Spin } from "antd";
import { Content } from "antd/es/layout/layout";
import { Header } from "@/view/components/Header/Header.tsx";
import type { Fine } from "@/domain/Fine.ts";
import {fetchFine} from "@/api";
import {FineForm} from "@/view/components/Forms/FineForm/FineForm.tsx";

export const FinePage = () => {
    const [loading, setLoading] = useState(true);
    const [fine, setFine] = useState<Fine>();
    const { id } = useParams<{ id: string }>();
    const fineId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setFine(await fetchFine(fineId));
            setLoading(false);
        };
        if (id) loadData();
    }, [fineId, id]);

    if (!id) return null;
    if (loading || !fine) return <Spin size="large" style={{ display: "block", margin: "100px auto" }} />;

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row justify="center" align="middle" style={{ minHeight: "calc(100vh - 64px)" }}>
                    <Col>
                        <FineForm fine={fine} />
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
