import type {Reader} from "@/domain/Reader.ts";

export type ReaderCategory = {
    id: number;
    name: string;
    discountRate?: number;
    Readers?: Reader[];
};