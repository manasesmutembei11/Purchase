import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MenuItem } from '../models/menu.model';


@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string) { }
  getMenu():Observable<MenuItem[]> {
    return this.http.get<MenuItem[]>(`${this.baseUrl}api/menu/menuItems`);
  }

}
