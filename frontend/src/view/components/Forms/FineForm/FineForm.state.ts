import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createFine, fetchRental, fetchRentals, updateFine } from "@/api";
import type { FineFormProps } from "./FineForm.tsx";
import type { Fine } from "@/domain/Fine.ts";
import type { Rental } from "@/domain/Rental.ts";

export const useFineFormState = ({ fine }: FineFormProps) => {
    const [rentals, setRentals] = useState<Rental[]>([]);
    const [rental, setRental] = useState<Rental>();
    const [disabled, setDisabled] = useState(!!fine);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const rentalsData = await fetchRentals();
            setRentals(rentalsData ?? []);

            if (fine?.rentalId) {
                const rentalData = await fetchRental(fine?.rentalId);
                setRental(rentalData);
            }
        };
        loadData();
    }, []);

    const save = async (formFine: Fine) => {
        let savedFine: Fine;
        if (fine?.id) {
            await updateFine(fine.id, formFine);
        } else {
            savedFine = await createFine(formFine);
            navigate(`/fines/${savedFine.id}`);
        }

        setDisabled(true);
    };

    return { fine, rental, rentals, disabled, setDisabled, save };
};
