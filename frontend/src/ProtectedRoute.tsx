import {Navigate, Outlet} from "react-router-dom";
import {useAuthUser} from "@/hooks/useAuthUser.ts";

type ProtectedRouteProps = {
    allowedRoles: string[];
    redirectPath?: string;
};

export const ProtectedRoute = ({
                                   allowedRoles,
                                   redirectPath = "/403",
                               }: ProtectedRouteProps) => {
    const user = useAuthUser();

    if (user.login == undefined) {
        return <Navigate to={redirectPath} replace/>;
    }

    const hasAccess = user?.roles?.some((role: string) => allowedRoles.includes(role));

    if (!hasAccess) {
        return <Navigate to={redirectPath} replace/>;
    }

    return <Outlet/>;
};
