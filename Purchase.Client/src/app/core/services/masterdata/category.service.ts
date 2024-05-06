import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Category } from '../../models/masterdata/category.model';

import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared/basic-response';
import { PagedList } from '../../models/shared/paged-list';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}
  public save = (body: Category): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Area/save',body);
  };
  findById(id: string): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}api/Area/${id}`);
  } 

  delete(id: string): Observable<Category> {
    return this.http.delete<Category>(`${this.baseUrl}api/Area/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Category>> => {
    return this.http.get<PagedList<Category>>(
      `${this.baseUrl}api/Area/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 
}