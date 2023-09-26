import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  constructor(private http: HttpClient) { }

  public getAll() {
    this.http.get('https://localhost:7002/api/logs?pageNumber=1&pageSize=10')
      .subscribe((data) => console.log(data));
  }
}
