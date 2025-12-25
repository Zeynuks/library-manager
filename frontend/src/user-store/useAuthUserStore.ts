import {useContext} from "react";
import {AuthUserContext} from "./AuthUserContext.tsx";

export const useAuthUserStore = () => useContext(AuthUserContext);
