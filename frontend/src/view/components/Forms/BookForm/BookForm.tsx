import { BookFormView } from './BookForm.view.tsx';
import { useBookFormState } from './BookForm.state.ts';
import type {Book} from "@/domain/Book.ts";

export type BookFormProps = {
    book?: Book
}

export const BookForm = ({book}: BookFormProps) => {
    return <BookFormView {...useBookFormState({book})} />;
};
