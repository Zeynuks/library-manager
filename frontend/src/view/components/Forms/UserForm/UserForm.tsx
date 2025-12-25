import { UserFormView } from "./UserForm.view.tsx";
import { useUserFormState } from "./UserForm.state.ts";
import type { User } from "@/domain";

export type UserFormProps = {
    user?: User;
};

export const UserForm = ({ user }: UserFormProps) => {
    return <UserFormView {...useUserFormState({ user })} />;
};
