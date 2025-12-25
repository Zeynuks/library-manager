import axios from 'axios';
import {handleError} from "@/errorHandler.ts";

export const api = axios.create({
    baseURL: 'http://localhost:5251',
    withCredentials: true,
    headers: { 'Content-Type': 'application/json' },
});

api.interceptors.response.use(
    response => response,
    error => {
        handleError(error);
        return Promise.reject(error);
    }
);