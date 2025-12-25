import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createBook, fetchTariffs, updateBook } from "@/api";
import type { BookFormProps } from "@/view/components/Forms/BookForm/BookForm.tsx";
import type { Book } from "@/domain/Book.ts";
import type { Tariff } from "@/domain/Tariff.ts";

export const useBookFormState = ({ book }: BookFormProps) => {
    const [tariffs, setTariffs] = useState<Tariff[]>([]);
    const [disabled, setDisabled] = useState(!!book);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const tariffs = await fetchTariffs();
            setTariffs(tariffs ?? []);
        };
        loadData();
    }, []);

    const save = async (formBook: Book) => {
        let savedBook: Book;
        if (book?.id) {
            await updateBook(book.id, formBook );
        } else {
            savedBook = await createBook(formBook);
            navigate(`/books/${savedBook.id}`);
        }

        setDisabled(true);
    };

    return { book, tariffs, disabled, setDisabled, save };
};
