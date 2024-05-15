import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Product } from '../../models/masterdata-models/masterdata.models';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';
import { PagedList } from '../../models/shared-models/paged-list';


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  public products: Product[] = [];
  private baseUrl = 'https://localhost:7224/'; 
  constructor(
    private http: HttpClient,

  ) {}
  public save = (body: Product): Observable<BasicResponse> => {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Product/Save',body);
  };
  findById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}api/Product/${id}`);
  } 

  delete(id: string): Observable<Product> {
    return this.http.delete<Product>(`${this.baseUrl}api/Product/${id}`);
  } 
  
  public list = (  
    pageNumber: number,
    pageSize: number,
    search: string
  ): Observable<PagedList<Product>> => {
    return this.http.get<PagedList<Product>>(
      `${this.baseUrl}api/Product/pagedlist?pageNumber=${pageNumber}&pageSize=${pageSize}&search=${search}`
    );
  }; 


  decreaseProductQuantity(id: string, quantity: number): Observable<any> {
    return this.http.put(`${this.baseUrl}api/Product/${id}/DecreaseQuantity`, { quantity });
  }

  increaseProductQuantity(id: string, quantity: number): Observable<any> {
    return this.http.put(`${this.baseUrl}api/Product/${id}/IncreaseQuantity`, { quantity });
  }

}