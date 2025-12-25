import { useState } from "react";
import { returnRentalBook } from "@/api";
import dayjs from "dayjs";
import type {RentalReturnFormProps} from "@/view/components/Forms/RentalReturnForm/RentalReturnForm.tsx";

export const useRentalReturnFormState = ({ rental }: RentalReturnFormProps) => {
    const [loading, setLoading] = useState(false);
    const [totalAmount, setTotalAmount] = useState<number | null>(null);

    const submit = async (actualReturnDate: dayjs.Dayjs) => {
        setLoading(true);
        try {
            const amount = await returnRentalBook(
                rental.id,
                actualReturnDate.toISOString().split("T")[0]
            );
            setTotalAmount(amount);
        } finally {
            setLoading(false);
        }
    };

    return {
        rental,
        submit,
        loading,
        totalAmount,
    };
};
