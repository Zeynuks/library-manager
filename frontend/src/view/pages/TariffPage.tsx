import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Col, Layout, Row, Spin } from "antd";
import { Content } from "antd/es/layout/layout";
import { Header } from "@/view/components/Header/Header";
import { TariffForm } from "@/view/components/Forms/TariffForm/TariffForm";
import { fetchTariff } from "@/api";
import type { Tariff } from "@/domain/Tariff";

export const TariffPage = () => {
    const [loading, setLoading] = useState(true);
    const [tariff, setTariff] = useState<Tariff>();
    const { id } = useParams<{ id: string }>();
    const tariffId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setTariff(await fetchTariff(tariffId));
            setLoading(false);
        };
        loadData();
    }, [tariffId]);

    if (!id) return null;
    if (loading || !tariff) {
        return <Spin size="large" style={{ display: "block", margin: "100px auto" }} />;
    }

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row
                    justify="center"
                    align="middle"
                    style={{ minHeight: "calc(100vh - 64px)" }}
                >
                    <Col>
                        <TariffForm tariff={tariff} />
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
