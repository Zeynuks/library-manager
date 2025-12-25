import { ReaderListView } from './ReaderList.view.tsx';
import { useReaderListState } from './ReaderList.state.ts';

export const ReaderList = () => {
    const state = useReaderListState();
    return <ReaderListView {...state} />;
};
