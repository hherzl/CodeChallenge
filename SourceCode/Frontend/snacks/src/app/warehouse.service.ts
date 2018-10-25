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

  public getProducts(name: string): Observable<Object> {
    const url = name == null ? [this.baseUrl, 'Product'].join('/') : [this.baseUrl, 'Product?name=' + name].join('/');

    return this.httpClient.get(url);
  }
}
