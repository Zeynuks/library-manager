import type {Rental} from "@/domain/Rental.ts";
import type {Reader} from "@/domain/Reader.ts";

export type Fine = {
    id: number;
    rentalId?: number;
    readerId?: number;
    Description: string;
    Amount: number;
    rental?: Rental;
    reader?: Reader;
};