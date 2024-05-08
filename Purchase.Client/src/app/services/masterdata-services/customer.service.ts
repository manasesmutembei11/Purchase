import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Customer } from '../../models/masterdata-models/masterdata.models';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: Customer): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Customer/Save',body);
  };
  findById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.baseUrl}api/Customer/${id}`);
  } 

  delete(id: string): Observable<Customer> {
    return this.http.delete<Customer>(`${this.baseUrl}api/Customer/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Customer>> => {
    return this.http.get<PagedList<Customer>>(
      `${this.baseUrl}api/Customer/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
}