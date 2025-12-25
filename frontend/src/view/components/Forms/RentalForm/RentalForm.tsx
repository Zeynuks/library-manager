import { RentalFormView } from './RentalForm.view.tsx';
import { useRentalFormState } from './RentalForm.state.ts';
import type { Rental } from "@/domain/Rental.ts";

export type RentalFormProps = {
    rental?: Rental
};

export const RentalForm = ({ rental }: RentalFormProps) => {
    return <RentalFormView {...useRentalFormState({ rental })} />;
};
