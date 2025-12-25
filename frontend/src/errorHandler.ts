import { message } from 'antd';
import type { AxiosError } from 'axios';

type ApiError = {
    message?: string;
    code?: string;
};

export function handleError(error: unknown): void {
    if (!error) {
        message.error('Неизвестная ошибка');
        return;
    }

    const axiosError = error as AxiosError<ApiError>;
    const status = axiosError.response?.status;
    const backendMessage = axiosError.response?.data?.message;

    switch (status) {
        case 400:
            message.warning(backendMessage ?? 'Некорректный запрос');
            break;

        case 401:
            message.error('Необходима авторизация');
            break;

        case 403:
            message.error('Доступ запрещён');
            break;

        case 404:
            message.error('Ресурс не найден');
            break;

        case 409:
            message.warning(backendMessage ?? 'Конфликт данных');
            break;

        case 422:
            message.warning('Ошибка валидации данных');
            break;

        case 500:
            message.error('Ошибка сервера. Попробуйте позже');
            break;

        default:
            message.error(backendMessage ?? 'Произошла ошибка');
    }
}
