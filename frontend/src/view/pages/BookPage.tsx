import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {Col, Layout, Row, Spin} from "antd";
import {Content} from "antd/es/layout/layout";
import {Header} from "@/view/components/Header/Header.tsx";
import {BookForm} from "@/view/components/Forms/BookForm/BookForm.tsx";
import {fetchBook} from "@/api";
import type {Book} from "@/domain";

export const BookPage = () => {
    const [loading, setLoading] = useState(true);
    const [book, setBook] = useState<Book>();
    const {id} = useParams<{ id: string }>();
    const bookId = Number(id);

    useEffect(() => {
        const loadData = async () => {
            setBook(await fetchBook(bookId));
            setLoading(false)
        };

        loadData();
    }, [bookId]);

    if (!id) return null;
    if (loading || !book) return <Spin size="large" style={{display: 'block', margin: '100px auto'}}/>;

    return (
        <Layout style={{minHeight: "100vh"}}>
            <Header/>
            <Content>
                <Row justify="center" align="middle" style={{minHeight: "calc(100vh - 64px)"}}>
                    <Col>
                        <BookForm book={book}/>
                    </Col>
                </Row>
            </Content>
        </Layout>
    );
};
