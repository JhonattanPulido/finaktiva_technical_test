import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Pagination, PaginationOutput } from '../models/pagination.model';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  private webApiUrl: string = 'https://localhost:7002/api/logs'

  constructor(private http: HttpClient) { }

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
      .set('finalDate', pagination.finalDate.toISOString().split('T')[0]);

    return this.http.get<PaginationOutput>(this.webApiUrl, { params: params });
  }
}
