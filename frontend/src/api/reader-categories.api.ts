import { message } from "antd";
import { api } from "@/api/api.ts";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";
import { showApiError } from "@/api/helper.ts";

export const fetchReaderCategory = async (id: number) => {
    try {
        const response = await api.get<ReaderCategory>(
            `api/reader-categories/${id}`
        );
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке категории читателя");
    }
};

export const fetchReaderCategories = async () => {
    try {
        const response = await api.get<ReaderCategory[]>(
            "api/reader-categories"
        );
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке списка категорий читателей");
    }
};

export const createReaderCategory = async (
    category: ReaderCategory
): Promise<ReaderCategory> => {
    try {
        const response = await api.post<ReaderCategory>(
            "api/reader-categories",
            category
        );
        message.success("Категория читателя успешно создана");
        return response.data;
    } catch (error) {
        showApiError(error, "Не удалось создать категорию читателя");
        throw error;
    }
};

export const updateReaderCategory = async (
    id: number,
    category: ReaderCategory
): Promise<void> => {
    try {
        await api.put(`api/reader-categories/${id}`, category);
        message.success("Категория читателя успешно обновлена");
    } catch (error) {
        showApiError(error, "Не удалось обновить категорию читателя");
        throw error;
    }
};

export const deleteReaderCategory = async (id: number): Promise<void> => {
    try {
        await api.delete(`api/reader-categories/${id}`);
        message.success("Категория читателя успешно удалена");
    } catch (error) {
        showApiError(error, "Не удалось удалить категорию читателя");
        throw error;
    }
};
