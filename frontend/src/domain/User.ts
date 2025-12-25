import type {UserRole} from "@/domain/UserRoles.ts";

export type User = {
    id: number;
    login: string;
    password: string;
    roles: UserRole[];
    isBlocked: boolean;
}