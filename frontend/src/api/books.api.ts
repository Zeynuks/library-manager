import { message } from "antd";
import { api } from "@/api/api.ts";
import type { Book, Rental } from "@/domain";
import { showApiError } from "@/api/helper.ts";

export const fetchBooks = async () => {
    try {
        const response = await api.get<Book[]>("api/books");
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных книги");
    }
};

export const fetchBook = async (bookId: number) => {
    try {
        const response = await api.get<Book>(`api/books/${bookId}`);
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных книги");
    }
};

export const fetchRentalsByBook = async (bookId: number) => {
    try {
        const response = await api.get<Rental[]>(
            `api/books/${bookId}/rentals`
        );
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке аренд книги");
    }
};

export const createBook = async (book: Book): Promise<Book> => {
    try {
        const response = await api.post<Book>("api/books", book);
        message.success("Книга успешно создана");
        return response.data;
    } catch (error) {
        showApiError(error, "Не удалось создать книгу");
        throw error;
    }
};

export const updateBook = async (
    bookId: number,
    book: Book
): Promise<void> => {
    try {
        await api.put(`api/books/${bookId}`, book);
        message.success("Книга успешно обновлена");
    } catch (error) {
        showApiError(error, "Не удалось обновить книгу");
        throw error;
    }
};

export const deleteBook = async (bookId: number): Promise<void> => {
    try {
        await api.delete(`api/books/${bookId}`);
        message.success("Книга успешно удалена");
    } catch (error) {
        showApiError(error, "Не удалось удалить книгу");
        throw error;
    }
};

export const fetchBookOccupied = async (bookId: number) => {
    try {
        const response = await api.get<boolean>(
            `api/books/${bookId}/occupied`
        );
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при проверке занятости книги");
    }
};
