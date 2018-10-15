import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient) {
    this.baseUrl = 'http://localhost:5700/api/v1/Warehouse';
  }

  public getProducts(): Observable<Object> {
    return this.httpClient.get([this.baseUrl].join('/'));
  }
}
