import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import type { User } from "@/domain/User.ts";
import { fetchUsers, deleteUser } from "@/api";
import type {UserRole} from "@/domain/UserRoles.ts";

export const useUserListState = () => {
    const [users, setUsers] = useState<User[]>([]);
    const roles: UserRole[] = ["Administrator", "Manager", "Operator"];
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const users = await fetchUsers();
            setUsers(users ?? []);
            setLoading(false);
        };
        loadData();
    }, []);

    const removeUser = async (id: number) => {
        await deleteUser(id);
        setUsers((prev) => prev.filter(u => u.id !== id));
    };

    return { users, roles, loading, navigate, removeUser };
};
