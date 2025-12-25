import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { fetchReaderCategories, deleteReaderCategory as deleteRC } from "@/api";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";

export const useReaderCategoryListState = () => {
    const [categories, setCategories] = useState<ReaderCategory[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const data = await fetchReaderCategories();
            setCategories(data ?? []);
            setLoading(false);
        };
        loadData();
    }, []);

    const deleteCategory = async (id: number) => {
        await deleteRC(id);
        setCategories((prev) => prev.filter(c => c.id !== id));
    };

    return { categories, loading, navigate, deleteCategory };
};
