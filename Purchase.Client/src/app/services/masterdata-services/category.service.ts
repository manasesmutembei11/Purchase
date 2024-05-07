import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Category } from '../../models/masterdata-models/category';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: Category): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Category/Save',body);
  };
  findById(id: string): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}api/Category/${id}`);
  } 

  delete(id: string): Observable<Category> {
    return this.http.delete<Category>(`${this.baseUrl}api/Category/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Category>> => {
    return this.http.get<PagedList<Category>>(
      `${this.baseUrl}api/Category/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
}