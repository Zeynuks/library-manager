import { FineFormView } from "./FineForm.view.tsx";
import { useFineFormState } from "./FineForm.state.ts";
import type { Fine } from "@/domain/Fine.ts";

export type FineFormProps = {
    fine?: Fine;
};

export const FineForm = ({ fine }: FineFormProps) => {
    return <FineFormView {...useFineFormState({ fine })} />;
};
