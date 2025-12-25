import { message } from "antd";
import { api } from "@/api/api.ts";
import type { Rental } from "@/domain/Rental.ts";
import { showApiError } from "@/api/helper.ts";

export const fetchRental = async (id: number) => {
    try {
        const response = await api.get<Rental>(`api/rentals/${id}`);
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных проката");
    }
};

export const fetchRentals = async () => {
    try {
        const response = await api.get<Rental[]>("api/rentals");
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при загрузке списка прокатов");
    }
};

export const createRental = async (rental: Rental): Promise<Rental> => {
    try {
        const response = await api.post<Rental>("api/rentals", rental);
        message.success("Аренда успешно создана");
        return response.data;
    } catch (error) {
        showApiError(error, "Не удалось создать аренду");
        throw error;
    }
};

export const updateRental = async (
    id: number,
    rental: Rental
): Promise<void> => {
    try {
        await api.put(`api/rentals/${id}`, rental);
        message.success("Аренда успешно обновлена");
    } catch (error) {
        showApiError(error, "Не удалось обновить аренду");
        throw error;
    }
};

export const returnRentalBook = async (
    id: number,
    actualReturnDate: string
) => {
    try {
        const response = await api.post<number>(
            `api/rentals/${id}/return`,
            { actualReturnDate }
        );
        message.success("Книга успешно возвращена");
        return response.data;
    } catch (error) {
        showApiError(error, "Ошибка при возврате книги");
        throw error;
    }
};

export const deleteRental = async (id: number): Promise<void> => {
    try {
        await api.delete(`api/rentals/${id}`);
        message.success("Прокат успешно удалён");
    } catch (error) {
        showApiError(error, "Ошибка при удалении проката");
        throw error;
    }
};
