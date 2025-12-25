import {useAuthUserStore} from "@/user-store";
import {useStore} from "@/store/useStore.ts";

export const useAuthUser = () => useStore(useAuthUserStore(), (state) => state);
