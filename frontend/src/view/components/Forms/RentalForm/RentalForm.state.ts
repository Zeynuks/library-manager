import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createRental, updateRental, fetchBooks, fetchReaders } from "@/api";
import type { RentalFormProps } from "./RentalForm.tsx";
import type { Rental } from "@/domain/Rental.ts";
import type { Book } from "@/domain/Book.ts";
import type { Reader } from "@/domain/Reader.ts";

export const useRentalFormState = ({ rental }: RentalFormProps) => {
    const [books, setBooks] = useState<Book[]>([]);
    const [readers, setReaders] = useState<Reader[]>([]);
    const [disabled, setDisabled] = useState(!!rental);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            setBooks(await fetchBooks() ?? []);
            setReaders(await fetchReaders() ?? []);
        };
        loadData();
    }, []);

    const save = async (formRental: Rental) => {
        if (rental?.id) {
            await updateRental(rental.id, formRental);
        } else {
            const result = await createRental(formRental);
            navigate(`/rentals/${result.id ?? rental?.id}`);
        }

        setDisabled(true);
    };

    return { rental, books, readers, disabled, setDisabled, save };
};
