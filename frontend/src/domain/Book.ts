import type {Tariff} from "@/domain/Tariff.ts";

export type Book = {
    id: number;
    title: string;
    author: string;
    genre: string;
    deposit: number;
    tariffId?: number;
    tariff?: Tariff;
}