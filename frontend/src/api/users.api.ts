import { message } from "antd";
import { api } from "@/api/api.ts";
import type { User } from "@/domain";
import type { UserRole } from "@/domain/UserRoles.ts";
import { showApiError } from "@/api/helper.ts";

export type UserResponse = {
    id: number;
    login: string;
    password: string;
    role: UserRole;
    isBlocked: boolean;
};

const mapUser = (dto: UserResponse): User => ({
    id: dto.id,
    login: dto.login,
    password: dto.password,
    roles: [dto.role],
    isBlocked: dto.isBlocked,
});

export const fetchUsers = async (): Promise<User[]> => {
    try {
        const response = await api.get<UserResponse[]>("api/users");
        return response.data.map(mapUser);
    } catch (error) {
        showApiError(error, "Ошибка при загрузке пользователей");
        return [];
    }
};

export const fetchUser = async (
    userId: number
): Promise<User | undefined> => {
    try {
        const response = await api.get<UserResponse>(
            `api/users/${userId}`
        );
        return mapUser(response.data);
    } catch (error) {
        showApiError(error, "Ошибка при загрузке данных пользователя");
    }
};

export const createUser = async (user: User): Promise<User> => {
    try {
        const payload = {
            ...user,
            role: user.roles[0],
        };

        const response = await api.post<UserResponse>(
            "api/users",
            payload
        );
        message.success("Пользователь успешно создан");
        return mapUser(response.data);
    } catch (error) {
        showApiError(error, "Не удалось создать пользователя");
        throw error;
    }
};

export const updateUser = async (
    id: number,
    user: User
): Promise<void> => {
    try {
        const payload = {
            ...user,
            role: user.roles[0],
        };

        await api.put<UserResponse>(`api/users/${id}`, payload);
        message.success("Пользователь успешно обновлён");
    } catch (error) {
        showApiError(error, "Не удалось обновить пользователя");
        throw error;
    }
};

export const deleteUser = async (userId: number): Promise<void> => {
    try {
        await api.delete(`api/users/${userId}`);
        message.success("Пользователь успешно удалён");
    } catch (error) {
        showApiError(error, "Не удалось удалить пользователя");
        throw error;
    }
};

export const blockUser = async (userId: number): Promise<void> => {
    try {
        await api.post(`api/users/${userId}/block`);
        message.success("Пользователь успешно заблокирован");
    } catch (error) {
        showApiError(error, "Не удалось заблокировать пользователя");
        throw error;
    }
};

export const unblockUser = async (userId: number): Promise<void> => {
    try {
        await api.post(`api/users/${userId}/unblock`);
        message.success("Пользователь успешно разблокирован");
    } catch (error) {
        showApiError(error, "Не удалось разблокировать пользователя");
        throw error;
    }
};
