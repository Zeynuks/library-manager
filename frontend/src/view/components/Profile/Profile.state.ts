import { useState, useEffect } from 'react';
import { message } from 'antd';
import { api } from '@/api/api.ts';
import {useNavigate} from "react-router-dom";

export type User = {
    login: string;
    roles: string[];
};

export const useProfileState = () => {
    const [user, setUser] = useState<User | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const { data } = await api.get<User>('/account/me');
                setUser(data);
            } catch {
                setUser(null);
                navigate('/login');
                message.error('Не авторизован');
            }
        };

        fetchUser();
    }, []);

    return { user };
};
