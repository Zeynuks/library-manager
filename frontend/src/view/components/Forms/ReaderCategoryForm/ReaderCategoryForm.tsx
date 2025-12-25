import { ReaderCategoryFormView } from "./ReaderCategoryForm.view.tsx";
import { useReaderCategoryFormState } from "./ReaderCategoryForm.state.ts";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";

export type ReaderCategoryFormProps = {
    category?: ReaderCategory;
};

export const ReaderCategoryForm = ({ category }: ReaderCategoryFormProps) => {
    return <ReaderCategoryFormView {...useReaderCategoryFormState({ category })} />;
};
