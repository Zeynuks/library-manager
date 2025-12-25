import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import type { Rental } from "@/domain/Rental.ts";
import { fetchRentals, deleteRental as deleteR } from "@/api";

export const useRentalListState = () => {
    const [rentals, setRentals] = useState<Rental[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const rentals = await fetchRentals();
            setLoading(false);
            setRentals(rentals ?? []);
        };

        loadData();
    }, []);

    const deleteRental = async (id: number) => {
        await deleteR(id);
        setRentals(prev => prev.filter(r => r.id !== id));
    };

    return { rentals, loading, navigate, deleteRental };
};
