import { Observable } from 'rxjs';
import { Log } from '../models/log.model';
import { Injectable } from '@angular/core';
import { LogType } from '../enums/log.enum';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationOutput } from '../models/pagination.model';

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  private webApiUrl: string = 'https://localhost:7002/api/logs'

  constructor(private http: HttpClient) { }

  /**
   * Create event log
   * @param log Event log data
   */
  public create(log: Log) : Observable<Object> {
    return this.http.post(this.webApiUrl, log);
  }

  /**
   * Get paginated logs
   * @param pagination Pagination information
   * @returns Paginated logs
   */
  public getAll(pagination: Pagination) : Observable<PaginationOutput> {
    const params: HttpParams = new HttpParams()
      .set('pageNumber', pagination.pageNumber)
      .set('pageSize', pagination.pageSize)
      .set('initialDate', pagination.initialDate.toISOString().split('T')[0])
      .set('finalDate', pagination.finalDate.toISOString().split('T')[0])
      .set('type', pagination.type == LogType.All ? '' : pagination.type.toString());

    return this.http.get<PaginationOutput>(this.webApiUrl, { params: params });
  }
}
