import {createContext} from "react";
import {AppStorage} from "../AppStorage";
import {Store} from "@/store/Store.ts";
import type {AuthUser} from "@/domain";

export const AuthUserContext = createContext<Store<AuthUser>>(Store.create(AppStorage.getUser() ?? {
    login: undefined,
    roles: [],
    isBlocked: false,
}));
