import {useState} from "react";
import type {UserFormProps} from "./UserForm.tsx";
import {blockUser, createUser, unblockUser, updateUser, type UpdateUser} from "@/api";


export const useUserFormState = ({user}: UserFormProps) => {
    const [disabled, setDisabled] = useState(!!user);

    const save = async (formUser: UpdateUser) => {
        if (user?.id) {
            await updateUser(formUser);

            if (formUser.isBlocked !== user.isBlocked) {
                if (formUser.isBlocked) {
                    await blockUser(user.id);
                } else {
                    await unblockUser(user.id);
                }
            }

            setDisabled(true);
        } else {
            await createUser({
                login: formUser.login,
                password: formUser.password!,
                roleId: formUser.role.id
            });
        }
    };

    return {
        user,
        disabled,
        setDisabled,
        save
    };
};
