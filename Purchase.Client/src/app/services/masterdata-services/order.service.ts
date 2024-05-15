import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Order } from '../../models/masterdata-models/masterdata.models';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: Order): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Order/Save',body);
  };
  findById(id: string): Observable<Order> {
    return this.http.get<Order>(`${this.baseUrl}api/Order/${id}`);
  } 

  delete(id: string): Observable<Order> {
    return this.http.delete<Order>(`${this.baseUrl}api/Order/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Order>> => {
    return this.http.get<PagedList<Order>>(
      `${this.baseUrl}api/Order/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
  
  cancelOrder(id: string): Observable<BasicResponse> {
    return this.http.delete<BasicResponse>(`${this.baseUrl}api/Order/${id}`);
  }
}