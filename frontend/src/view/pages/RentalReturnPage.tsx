import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Col, Layout, Row, Spin } from "antd";
import { Content } from "antd/es/layout/layout";
import { Header } from "@/view/components/Header/Header.tsx";
import { fetchRental } from "@/api";
import type { Rental } from "@/domain/Rental.ts";
import {RentalReturnForm} from "@/view/components/Forms/RentalReturnForm/RentalReturnForm.tsx";

export const RentalReturnPage = () => {
    const [loading, setLoading] = useState(true);
    const [rental, setRental] = useState<Rental>();
    const { id } = useParams<{ id: string }>();
    const rentalId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setRental(await fetchRental(rentalId));
            setLoading(false);
        };

        loadData();
    }, [rentalId]);

    if (!id) return null;
    if (loading || !rental) return <Spin size="large" style={{ display: 'block', margin: '100px auto' }} />;

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header />
            <Content>
                <Row justify="center" align="middle" style={{ minHeight: "calc(100vh - 64px)" }}>
                    <Col>
                        <RentalReturnForm rental={rental} />
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
