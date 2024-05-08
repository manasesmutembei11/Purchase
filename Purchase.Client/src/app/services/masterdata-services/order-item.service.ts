import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Order, OrderItem } from '../../models/masterdata-models/masterdata.models';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class OrderItemService {
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: OrderItem): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/OrderItem/Save',body);
  };
  findById(id: string): Observable<OrderItem> {
    return this.http.get<OrderItem>(`${this.baseUrl}api/OrderItem/${id}`);
  } 

  delete(id: string): Observable<OrderItem> {
    return this.http.delete<OrderItem>(`${this.baseUrl}api/OrderItem/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<OrderItem>> => {
    return this.http.get<PagedList<OrderItem>>(
      `${this.baseUrl}api/OrderItem/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
}