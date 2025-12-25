import {message} from 'antd';
import {api} from '@/api/api.ts';
import {useNavigate} from 'react-router-dom';
import {useAuthUser} from "@/hooks/useAuthUser.ts";
import {useAuthUserStore} from "@/user-store";

export const useHeaderState = () => {
    const navigate = useNavigate();
    const user = useAuthUser()
    const userStore = useAuthUserStore()
    const onLogout = async () => {
        try {
            await api.post('/account/logout');
            userStore.set({
                login: undefined,
                roles: [],
                isBlocked: false,
            })
            message.success('Вы вышли из системы');
            navigate('/login');
        } catch {
            message.error('Ошибка при выходе');
        }
    };

    return {user, onLogout, navigate};
};
