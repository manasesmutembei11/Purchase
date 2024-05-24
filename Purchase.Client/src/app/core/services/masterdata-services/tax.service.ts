import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Tax } from '../../models/masterdata-models/masterdata.models';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class TaxService {
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: Tax): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Tax/Save',body);
  };
  findById(id: string): Observable<Tax> {
    return this.http.get<Tax>(`${this.baseUrl}api/Tax/${id}`);
  } 

  delete(id: string): Observable<Tax> {
    return this.http.delete<Tax>(`${this.baseUrl}api/Tax/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Tax>> => {
    return this.http.get<PagedList<Tax>>(
      `${this.baseUrl}api/Tax/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
}