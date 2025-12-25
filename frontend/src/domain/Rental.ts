import type {Book} from "@/domain/Book.ts";
import type {Reader} from "@/domain/Reader.ts";
import type { Dayjs } from "dayjs";

export type Rental = {
    id: number;
    bookId?: number;
    readerId?: number;
    issueDate: string | Dayjs;
    expectedReturnDate: string | Dayjs;
    actualReturnDate?: string | Dayjs;
    rentalAmount: number;
    book?: Book;
    reader?: Reader;
};