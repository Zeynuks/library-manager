import type {Rental} from "@/domain/Rental.ts";
import type {ReaderCategory} from "@/domain/ReaderCategory.ts";

export type Reader = {
    id: number;
    firstName: string;
    middleName?: string;
    lastName: string;
    address: string;
    phoneNumber: string;
    categoryId?: number;
    category?: ReaderCategory;
    rentals?: Rental[];
}