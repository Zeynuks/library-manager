import { message } from "antd";
import { api } from "@/api/api.ts";
import type { User } from "@/domain";
import type { Role } from "@/domain/Role.ts";

export type CreateUser = {
    login: string;
    password: string;
    roleId: Role["id"];
};

export type UpdateUser = User & {
    password?: string;
};

export const fetchUsers = async () => {
    try {
        const response = await api.get<User[]>("api/users");
        return response.data;
    } catch {
        message.error("Ошибка при загрузке пользователей");
    }
};

export const fetchUser = async (userId: number) => {
    try {
        const response = await api.get<User>(`api/users/${userId}`);
        return response.data;
    } catch {
        message.error("Ошибка при загрузке данных пользователя");
    }
};

export const createUser = async (user: CreateUser): Promise<User> => {
    try {
        const response = await api.post<User>("api/users", user);
        message.success("Пользователь успешно создан");
        return response.data;
    } catch {
        throw new Error();
    }
};

export const updateUser = async (user: UpdateUser): Promise<void> => {
    try {
        await api.put(`api/users/${user.id}`, user);
        message.success("Пользователь успешно обновлён");
    } catch {
        throw new Error();
    }
};

export const deleteUser = async (userId: number): Promise<void> => {
    try {
        await api.delete(`api/users/${userId}`);
        message.success("Пользователь успешно удалён");
    } catch {
        throw new Error();
    }
};

export const blockUser = async (userId: number): Promise<void> => {
    try {
        await api.post(`api/users/${userId}/block`);
        message.success("Пользователь заблокирован");
    } catch {
        throw new Error();
    }
};

export const unblockUser = async (userId: number): Promise<void> => {
    try {
        await api.post(`api/users/${userId}/unblock`);
        message.success("Пользователь разблокирован");
    } catch {
        throw new Error();
    }
};
