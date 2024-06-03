import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CustomEncoder } from '../shared-services/custom-encoder';
import { AuthResponseDto } from '../../models/auth-models/auth-response-dto';
import { AuthenticationDto } from '../../models/auth-models/authentication-dto';
import { ForgotPassword, User, ResetPassword } from '../../models/user-models/user';


@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  private baseUrl = 'https://localhost:7224/';
  constructor(
    private _http: HttpClient,
    
    private jwtHelper: JwtHelperService
  ) {
    this.currentUserSubject = new BehaviorSubject<User>(this.getUserInfo());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  public loginUser = (body: AuthenticationDto) => {
    return this._http
      .post<AuthResponseDto>(this.baseUrl + 'api/Auth/Login', body)
      .pipe(
        map((result) => {
          if (result && result.token) {
            localStorage.setItem('jwt', result.token);
            const user = this.getUserInfo();
            this.currentUserSubject.next(user);
          }
          return result;
        })
      );
  };
  public isUserAuthenticated = (): boolean => {
    // const token = localStorage.getItem("token");
    const token = this.getUserToken();
    //console.log('Token => ', token);
    const isvalid = token != null && !this.jwtHelper.isTokenExpired(token);
    return isvalid;
  };

  isInRole(role:any): boolean {

    if(!role){
      return true;
    }
    const token = this.getUserToken();
    const user = this.jwtHelper.decodeToken(token);
    if (!user || !user.role) {return false; }




    if (user.role instanceof Array) {
      return user.role.some((x: string) => x === role);
    }
    if (user.role === role) {
      return true;
    }
    return false;
  }
  hasPermission(permission:string): boolean{
    var user=this.currentUserValue
    console.log("permission => ",user.permissions);
    if(user && user.permissions){
      return user.permissions.some((x: string) => x === permission);
    }
    return false
  }
  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('jwt');
    this.currentUserSubject.next(this.getUserInfo());
  }
  private getUserInfo(): User | any {
    var user = {} as User;
    const token = this.getUserToken()
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      user.email = decodedToken.email;
      user.firstName=decodedToken.firstName;
      user.lastName=decodedToken.lastName;
      user.id=decodedToken.userId;
      user.permissions=this.getPermissions(decodedToken);

      return user;
    }
    return null;
  }
  private getPermissions(decodedToken: any): string[] {

    if (!decodedToken || !decodedToken.permission) {return []; }
    if (decodedToken.permission instanceof Array) {
      return decodedToken.permission;
    }
    return [];
  }
  getUserToken() :string  | any {
    return this.jwtHelper.tokenGetter()

  }
  public confirmEmail = ( token: string, email: string) => {
    let params = new HttpParams({ encoder: new CustomEncoder() })
    params = params.append('token', token);
    params = params.append('email', email);
    return this._http.get(this.baseUrl + 'api/Auth/emailconfirmation', { params: params });
  }

  public forgotPassword = ( body: ForgotPassword) => {
    return this._http.post( this.baseUrl + 'api/Auth/ForgotPassword',body);
  }
  public resetPassword = ( body: ResetPassword) => {
    return this._http.post( this.baseUrl + 'api/Auth/ResetPassword',body);
  }

}
