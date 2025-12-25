import { RentalListView } from './RentalList.view.tsx';
import { useRentalListState } from './RentalList.state.ts';

export const RentalList = () => {
    const state = useRentalListState();
    return <RentalListView {...state} />;
};
