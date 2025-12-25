import { message } from "antd";
import type { AxiosError } from "axios";

type ApiError = {
    message?: string;
};

export const showApiError = (error: unknown, fallback: string) => {
    const err = error as AxiosError<ApiError>;
    message.error(err.response?.data?.message ?? fallback);
};
