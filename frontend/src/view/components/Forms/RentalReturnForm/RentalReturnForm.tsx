import {RentalReturnFormView} from "./RentalReturnForm.view.tsx";
import {useRentalReturnFormState} from "./RentalReturnForm.state.ts";
import type {Rental} from "@/domain/Rental.ts";

export type RentalReturnFormProps = {
    rental: Rental;
};

export const RentalReturnForm = ({rental}: RentalReturnFormProps) => {
    return (
        <RentalReturnFormView {...useRentalReturnFormState({rental})} />
    );
};
