import { BookListView } from './BookList.view.tsx';
import { useBookListState } from './BookList.state.ts';

export const BookList = () => {
    const state = useBookListState();
    return <BookListView {...state} />;
};
