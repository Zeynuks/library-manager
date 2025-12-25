import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import type { Tariff } from "@/domain";
import { fetchTariffs, deleteTariff as deleteT } from "@/api";

export const useTariffListState = () => {
    const [tariffs, setTariffs] = useState<Tariff[]>([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const data = await fetchTariffs();
            setTariffs(data ?? []);
            setLoading(false);
        };

        loadData();
    }, []);

    const deleteTariff = async (id: number) => {
        await deleteT(id);
        setTariffs(prev => prev.filter(t => t.id !== id));
    };

    return { tariffs, loading, navigate, deleteTariff };
};
