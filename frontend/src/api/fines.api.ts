import {message} from "antd";
import {api} from "@/api/api.ts";
import type {Fine} from "@/domain/Fine.ts";

export const fetchFine = async (id: number) => {
    try {
        const response = await api.get<Fine>(`api/fines/${id}`);
        return response.data;
    } catch {
        message.error("Ошибка при загрузке данных штрафа");
    }
};

export const fetchFines = async () => {
    try {
        const response = await api.get<Fine[]>("api/fines");
        return response.data;
    } catch {
        message.error("Ошибка при загрузке списка штрафов");
    }
};

export const createFine = async (fine: Fine): Promise<Fine> => {
    try {
        const response = await api.post<Fine>("api/fines", fine);
        message.success("Штраф успешно создан");
        return response.data;
    } catch {
        throw new Error("Не удалось создать штраф");
    }
};

export const updateFine = async (id: number, fine: Fine): Promise<void> => {
    try {
        await api.put(`api/fines/${id}`, fine);
        message.success("Штраф успешно обновлён");
    } catch {
        throw new Error("Не удалось обновить штраф");
    }
};

export const deleteFine = async (id: number): Promise<void> => {
    try {
        await api.delete(`api/fines/${id}`);
        message.success("Штраф успешно удалён");
    } catch {
        throw new Error();
    }
};
