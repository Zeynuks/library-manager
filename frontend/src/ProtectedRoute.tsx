import {Navigate, Outlet} from "react-router-dom";
import {useAuthUser} from "@/hooks/useAuthUser.ts";
import type {UserRole} from "@/domain/UserRoles.ts";

type ProtectedRouteProps = {
    allowedRoles: UserRole[];
    redirectPath?: string;
};

export const ProtectedRoute = ({
                                   allowedRoles,
                                   redirectPath = "/403",
                               }: ProtectedRouteProps) => {
    const user = useAuthUser();

    if (user.login == undefined) {
        return <Navigate to={"/login"} replace/>;
    }

    const hasAccess = (user.roles as UserRole[]).some(role => allowedRoles.includes(role));

    if (!hasAccess) {
        return <Navigate to={redirectPath} replace/>;
    }

    return <Outlet/>;
};
