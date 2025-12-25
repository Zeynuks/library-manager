import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {Col, Layout, Row, Spin} from "antd";
import {Content} from "antd/es/layout/layout";
import {Header} from "@/view/components/Header/Header.tsx";
import type {ReaderCategory} from "@/domain/ReaderCategory.ts";
import {fetchReaderCategory} from "@/api";
import {ReaderCategoryForm} from "@/view/components/Forms/ReaderCategoryForm/ReaderCategoryForm.tsx";

export const ReaderCategoryPage = () => {
    const [loading, setLoading] = useState(true);
    const [category, setCategory] = useState<ReaderCategory>();
    const {id} = useParams<{ id: string }>();
    const categoryId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            if (id) {
                const data = await fetchReaderCategory(categoryId);
                setCategory(data);
                setLoading(false);
            }
        };
        loadData();
    }, [categoryId, id]);

    if (!id) return null;
    if (loading || !category) {
        return <Spin size="large" style={{display: "block", margin: "100px auto"}}/>;
    }

    return (
        <Layout style={{minHeight: "100vh"}}>
            <Header/>
            <Content>
                <Row justify="center" align="middle" style={{minHeight: "calc(100vh - 64px)"}}>
                    <Col>
                        <ReaderCategoryForm category={category}/>
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
