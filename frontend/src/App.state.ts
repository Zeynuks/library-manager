import {AppStorage} from "./AppStorage";
import {useSubscribeStore} from "./store";
import {useAuthUserStore} from "@/user-store";

export const useAppState = () => {
    useSubscribeStore(useAuthUserStore(), (value) => AppStorage.setUser(value));
};