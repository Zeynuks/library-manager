import { message } from "antd";
import { api } from "@/api/api.ts";
import type { Reader } from "@/domain/Reader.ts";
import { showApiError } from "@/api/helper.ts";

export const fetchReader = async (id: number) => {
    try {
        const response = await api.get<Reader>(`api/readers/${id}`);
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных читателя");
    }
};

export const fetchReaders = async () => {
    try {
        const response = await api.get<Reader[]>("api/readers");
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке списка читателей");
    }
};

export const fetchReaderRentals = async (id: number) => {
    try {
        const response = await api.get<Reader>(
            `api/readers/${id}/rentals`
        );
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке прокатов читателя");
    }
};

export const createReader = async (reader: Reader): Promise<Reader> => {
    try {
        const response = await api.post<Reader>("api/readers", reader);
        message.success("Читатель успешно создан");
        return response.data;
    } catch (error) {
        showApiError(error, "Не удалось создать читателя");
        throw error;
    }
};

export const updateReader = async (
    id: number,
    reader: Reader
): Promise<void> => {
    try {
        await api.put(`api/readers/${id}`, reader);
        message.success("Читатель успешно обновлён");
    } catch (error) {
        showApiError(error, "Не удалось обновить читателя");
        throw error;
    }
};

export const deleteReader = async (id: number): Promise<void> => {
    try {
        await api.delete(`api/readers/${id}`);
        message.success("Читатель успешно удалён");
    } catch (error) {
        showApiError(error, "Не удалось удалить читателя");
        throw error;
    }
};
