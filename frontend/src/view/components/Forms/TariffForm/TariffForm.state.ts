import { useNavigate } from "react-router-dom";
import { createTariff, updateTariff } from "@/api";
import type { Tariff } from "@/domain/Tariff";
import type { TariffFormProps } from "./TariffForm";
import {useState} from "react";

export const useTariffFormState = ({ tariff }: TariffFormProps) => {
    const [disabled, setDisabled] = useState(!!tariff);
    const navigate = useNavigate();

    const save = async (formTariff: Tariff) => {
        if (tariff?.id) {
            await updateTariff(tariff.id, formTariff);
        } else {
            const saved = await createTariff(formTariff);
            navigate(`/tariffs/${saved.id}`);
        }

        setDisabled(true);
    };

    return { tariff, disabled, setDisabled, save };
};
