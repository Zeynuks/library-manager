import type {AuthUser} from "./domain";

const localStorageKey = `dictionaryLocalStorage`;

const setUser = (value: AuthUser) => localStorage.setItem(localStorageKey, JSON.stringify(value));

const getUser = (): AuthUser | undefined => {
    const item = localStorage.getItem(localStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

export const AppStorage = {getUser, setUser};