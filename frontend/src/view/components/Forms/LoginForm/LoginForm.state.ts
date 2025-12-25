import {message} from "antd";
import {api} from "@/api/api.ts";
import type {LoginFormProps} from './LoginForm.tsx';
import {useNavigate} from "react-router-dom";
import {useAuthUserStore} from "@/user-store";
import type {AuthUser} from "@/domain";

type LoginData = {
    login: string;
    password: string;
};

export const useLoginFormState = ({
                                      disabled
                                  }: LoginFormProps) => {
    const navigate = useNavigate();
    const userStore = useAuthUserStore()

    const onFinish = async (values: LoginData) => {
        try {
            await api.post('/account/login', values);
            const response = await api.get<AuthUser>('/account/me');
            userStore.set(response.data);
            navigate('/profile');
            message.success('Успешный вход');
        } catch {
            message.error('Ошибка авторизации');
        }
    };

    return {
        onFinish,
        disabled
    };
};
