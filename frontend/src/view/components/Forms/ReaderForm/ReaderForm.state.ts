import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createReader, fetchReaderCategories, updateReader } from "@/api";
import type { ReaderFormProps } from "@/view/components/Forms/ReaderForm/ReaderForm.tsx";
import type { Reader } from "@/domain/Reader.ts";
import type { ReaderCategory } from "@/domain/ReaderCategory.ts";

export const useReaderFormState = ({ reader }: ReaderFormProps) => {
    const [categories, setCategories] = useState<ReaderCategory[]>([]);
    const [disabled, setDisabled] = useState(!!reader);
    const navigate = useNavigate();

    useEffect(() => {
        const loadData = async () => {
            const cats = await fetchReaderCategories();
            setCategories(cats ?? []);
        };
        loadData();
    }, []);

    const save = async (formReader: Reader) => {
        let savedReader: Reader;
        if (reader?.id) {
            await updateReader(reader.id, formReader);
        } else {
            savedReader = await createReader(formReader);
            navigate(`/readers/${savedReader.id}`);
        }

        setDisabled(true);
    };

    return { reader, categories, disabled, setDisabled, save };
};
