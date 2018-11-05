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

  public getProducts(pageSize: number, pageNumber: number, name: string): Observable<Object> {
    const parameters = [
      'pageSize=' + (pageSize == null ? '10' : pageSize.toString()),
      'pageNumber=' + (pageNumber == null ? '1' : pageNumber.toString())
    ];

    if (name != null) {
      parameters.push('name=' + name);
    }

    const url: string = [this.baseUrl, 'Product'].join('/') + '?' + parameters.join('&');

    console.log(url);

    return this.httpClient.get(url);
  }
}
