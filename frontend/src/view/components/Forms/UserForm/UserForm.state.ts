import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {blockUser, createUser, unblockUser, updateUser} from "@/api";
import type {User} from "@/domain/User.ts";
import type {UserFormProps} from "./UserForm.tsx";
import type {UserRole} from "@/domain/UserRoles.ts";

export const useUserFormState = ({user}: UserFormProps) => {
    const roles: UserRole[] = ["Administrator", "Manager", "Operator"];
    const [blocked, setBlocked] = useState<boolean | undefined>(user?.isBlocked);
    const [disabled, setDisabled] = useState(!!user);
    const navigate = useNavigate();
    const save = async (formUser: User) => {
        if (user?.id) {
            if (formUser.isBlocked != user.isBlocked) {
                formUser.isBlocked = !formUser.isBlocked;
            }
            await updateUser(user.id, formUser);
        } else {
            const savedUser = await createUser(formUser);
            navigate(`/users/${savedUser.id}`);
        }
        setDisabled(true);
    };

    const toggleBlockUser = () => {
        if (user?.id) {
            if (blocked) {
                unblockUser(user?.id);
            } else {
                blockUser(user?.id);
            }
            setBlocked(!blocked);
        }
    }

    return {user, roles, disabled, blocked, setDisabled, toggleBlockUser, save}
};