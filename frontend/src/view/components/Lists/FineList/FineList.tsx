import { FineListView } from './FineList.view.tsx';
import { useFineListState } from './FineList.state.ts';

export const FineList = () => {
    const state = useFineListState();
    return <FineListView {...state} />;
};
