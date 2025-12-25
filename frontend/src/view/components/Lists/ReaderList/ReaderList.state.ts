import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import type { Reader } from "@/domain/Reader.ts";
import { fetchReaders, deleteReader as deleteR } from "@/api";

export const useReaderListState = () => {
    const [readers, setReaders] = useState<Reader[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const data = await fetchReaders();
            setReaders(data ?? []);
            setLoading(false);
        };
        loadData();
    }, []);

    const deleteReader = async (id: number) => {
        await deleteR(id);
        setReaders(prev => prev.filter(r => r.id !== id));
    };

    return { readers, loading, navigate, deleteReader };
};
