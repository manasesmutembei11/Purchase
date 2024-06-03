import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MenuItem } from '../models/menu.model';


@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private baseUrl = 'https://localhost:7224/'

  constructor(private http: HttpClient) { }
  getMenu():Observable<MenuItem[]> {
    return this.http.get<MenuItem[]>(`${this.baseUrl}api/menu/menuItems`);
  }

}
