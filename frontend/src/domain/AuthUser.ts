export type AuthUser = {
    login: string | undefined;
    roles: string[];
    isBlocked: boolean;
};
