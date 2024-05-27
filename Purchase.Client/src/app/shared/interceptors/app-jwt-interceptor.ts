import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../core/services/auth-services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AppJwtInterceptor implements HttpInterceptor {

  constructor( private authService:AuthService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this.authService.currentUserValue;
    if (currentUser && this.authService.isUserAuthenticated()) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authService.getUserToken()}`
        }
      });
    }
    return next.handle(request);
  }
}
