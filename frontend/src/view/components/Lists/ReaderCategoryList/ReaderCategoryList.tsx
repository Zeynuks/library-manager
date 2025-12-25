import { ReaderCategoryListView } from './ReaderCategoryList.view.tsx';
import { useReaderCategoryListState } from './ReaderCategoryList.state.ts';

export const ReaderCategoryList = () => {
    const state = useReaderCategoryListState();
    return <ReaderCategoryListView {...state} />;
};
