import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { deleteFine as deleteF, fetchFines } from "@/api";
import type { Fine } from "@/domain/Fine.ts";

export const useFineListState = () => {
    const [fines, setFines] = useState<Fine[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const fines = await fetchFines();
            setFines(fines ?? []);
            setLoading(false);
        };
        loadData();
    }, []);

    const deleteFine = async (id: number) => {
        await deleteF(id);
        setFines((prev) => prev.filter(f => f.id !== id));
    };

    return { fines, loading, navigate, deleteFine };
};
