import { useEffect, useState } from "react";
import {useNavigate} from "react-router-dom";
import type {Book} from "@/domain";
import {fetchBooks, deleteBook as deleteB} from "@/api";

export const useBookListState = () => {
    const [books, setBooks] = useState<Book[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const books = await fetchBooks();
            setLoading(false);
            setBooks(books ?? []);
        };

        loadData();
    }, []);

    const deleteBook = async (id: number) => {
        await deleteB(id);
    }

    return { books, loading, navigate, deleteBook };
};
