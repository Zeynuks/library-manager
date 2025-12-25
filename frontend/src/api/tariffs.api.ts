import { message } from "antd";
import { api } from "@/api/api.ts";
import type { Tariff } from "@/domain/Tariff.ts";
import { showApiError } from "@/api/helper.ts";

export const fetchTariff = async (id: number) => {
    try {
        const response = await api.get<Tariff>(`api/tariffs/${id}`);
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных тарифа");
    }
};

export const fetchTariffs = async () => {
    try {
        const response = await api.get<Tariff[]>("api/tariffs");
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке списка тарифов");
    }
};

export const createTariff = async (tariff: Tariff): Promise<Tariff> => {
    try {
        const response = await api.post<Tariff>("api/tariffs", tariff);
        message.success("Тариф успешно создан");
        return response.data;
    } catch (error) {
        showApiError(error, "Не удалось создать тариф");
        throw error;
    }
};

export const updateTariff = async (
    id: number,
    tariff: Tariff
): Promise<void> => {
    try {
        await api.put(`api/tariffs/${id}`, tariff);
        message.success("Тариф успешно обновлён");
    } catch (error) {
        showApiError(error, "Не удалось обновить тариф");
        throw error;
    }
};

export const deleteTariff = async (id: number): Promise<void> => {
    try {
        await api.delete(`api/tariffs/${id}`);
        message.success("Тариф успешно удалён");
    } catch (error) {
        showApiError(error, "Не удалось удалить тариф");
        throw error;
    }
};
