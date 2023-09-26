import { LogType } from "../enums/log.enum";

export interface Log {
    id?: string;
    description: string;
    type: LogType;
    creationDate: Date;
}