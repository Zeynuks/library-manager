import { TariffListView } from "./TariffList.view";
import { useTariffListState } from "./TariffList.state";

export const TariffList = () => {
    const state = useTariffListState();
    return <TariffListView {...state} />;
};
