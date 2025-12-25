import { TariffFormView } from "./TariffForm.view";
import { useTariffFormState } from "./TariffForm.state";
import type { Tariff } from "@/domain/Tariff";

export type TariffFormProps = {
    tariff?: Tariff;
};

export const TariffForm = ({ tariff }: TariffFormProps) => {
    return <TariffFormView {...useTariffFormState({ tariff })} />;
};
