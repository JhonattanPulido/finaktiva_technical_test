import { LogType } from "../enums/log.enum";
import { Log } from "./log.model";

export interface Pagination {
    pageNumber: number;
    pageSize: number;
    type: LogType;
    initialDate: Date;
    finalDate: Date;
};

export interface PaginationOutput {
    pages: number;
    count: number;
    items: Log[]
};