import { message } from "antd";
import { api } from "@/api/api.ts";
import type { User } from "@/domain";
import type {UserRole} from "@/domain/UserRoles.ts";

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
    } catch {
        message.error("Ошибка при загрузке пользователей");
        return [];
    }
};

export const fetchUser = async (userId: number): Promise<User | undefined> => {
    try {
        const response = await api.get<UserResponse>(`api/users/${userId}`);
        return mapUser(response.data);
    } catch {
        message.error("Ошибка при загрузке данных пользователя");
    }
};

export const createUser = async (user: User): Promise<User> => {
    try {
        const payload = {
            ...user,
            role: user.roles[0],
        };

        const response = await api.post<UserResponse>("api/users", payload);
        message.success("Пользователь успешно создан");
        return mapUser(response.data);
    } catch {
        throw new Error();
    }
};

export const updateUser = async (id: number, user: User): Promise<void> => {
    try {
        const payload = {
            ...user,
            role: user.roles[0],
        };
        console.log(payload)

        await api.put<UserResponse>(`api/users/${id}`, payload);
        message.success("Пользователь успешно обвнолён");
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
        message.success("Пользователь успешно заблокирован");
    } catch {
        throw new Error();
    }
};

export const unblockUser = async (userId: number): Promise<void> => {
    try {
        await api.post(`api/users/${userId}/unblock`);
        message.success("Пользователь успешно разблокирован");
    } catch {
        throw new Error();
    }
};