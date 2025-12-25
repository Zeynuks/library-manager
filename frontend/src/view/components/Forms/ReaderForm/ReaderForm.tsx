import { ReaderFormView } from './ReaderForm.view.tsx';
import { useReaderFormState } from './ReaderForm.state.ts';
import type { Reader } from "@/domain/Reader.ts";

export type ReaderFormProps = {
    reader?: Reader
}

export const ReaderForm = ({ reader }: ReaderFormProps) => {
    return <ReaderFormView {...useReaderFormState({ reader })} />;
};
