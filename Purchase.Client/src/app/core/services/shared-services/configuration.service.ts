import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasicResponse } from '../../models/shared-models/basic-response';

import { AppVersion } from '../../models/shared-models/error';
import { EnumLookupItem, LookupItem } from '../../models/shared-models/lookup-item';


@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {


  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string) { }






  getAppVersion():Observable<AppVersion> {
    return this.http.get<AppVersion>(`${this.baseUrl}api/Configuration/`);
  }

  public  getConfigTypes = (): Observable<EnumLookupItem[]> => {
    return this.http.get<EnumLookupItem[]>(
      `${this.baseUrl}api/Configuration/ConfigTypes`
    );
  };

  public  getConfig = (configTypeId: number): Observable<any[]> => {
    return this.http.get<any[]>(
      `${this.baseUrl}api/Configuration/get/${configTypeId}`
    );
  };

  public save = ( body: any): Observable<BasicResponse >=> {
    return this.http.post<BasicResponse>(this.baseUrl + 'api/Configuration/save', body);
  }

  public ClaimNaturelookuplistLookupList = (): Observable<LookupItem[]> => {
    return this.http.get<LookupItem[]>(
      `${this.baseUrl}api/Configuration/ClaimNatureLookuplist/`
    );
  };


}
