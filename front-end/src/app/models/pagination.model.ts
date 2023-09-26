import { Log } from "./log.model";

export interface Pagination {
    pageNumber: number;
    pageSize: number;
    initialDate: Date;
    finalDate: Date;
};

export interface PaginationOutput {
    pages: number;
    count: number;
    items: Log[]
};