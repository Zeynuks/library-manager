import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createReaderCategory, fetchReaderCategory, updateReaderCategory } from "@/api";
import type { ReaderCategoryFormProps } from "./ReaderCategoryForm.tsx";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";

export const useReaderCategoryFormState = ({ category }: ReaderCategoryFormProps) => {
    const [currentCategory, setCurrentCategory] = useState<ReaderCategory>();
    const [disabled, setDisabled] = useState(!!category);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            if (category?.id) {
                const data = await fetchReaderCategory(category.id);
                setCurrentCategory(data);
            }
        };
        loadData();
    }, []);

    const save = async (formCategory: ReaderCategory) => {
        let savedCategory: ReaderCategory;
        if (category?.id) {
            await updateReaderCategory(category.id, formCategory);
        } else {
            savedCategory = await createReaderCategory(formCategory);
            navigate(`/reader-categories/${savedCategory.id}`);
        }

        setDisabled(true);
    };

    return { category: currentCategory, disabled, setDisabled, save };
};
