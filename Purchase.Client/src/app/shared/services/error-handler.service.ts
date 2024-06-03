import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Error } from '../../core/models/shared-models/error';


@Injectable({
  providedIn: 'root',
})
export class ErrorHandlerService {
  constructor(private _router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = this.handleError(error);
        return throwError(errorMessage);
      })
    );
  }
  handleError(error: HttpErrorResponse) {
    //console.log('handleError Status => ', error.status);
    
    if (error.status === 404) {
      return this.handleNotFound(error);
    } else if (error.status === 400) {
      return this.handleBadRequest(error);
    } else if (error.status === 422) {
      return this.handleUnprocessableEntity(error);
    }
    else if (error.status === 401) {
      return this.handleUnauthorized(error);
    }
    else if (error.status === 403) {
      return this.handleForbiden(error);
    }
    else{
         return this.handleOtherErrors(error);;
    }
  }
  handleForbiden(error: HttpErrorResponse) {
    this._router.navigate(['/Forbidden']);
  }
  handleUnprocessableEntity(error: HttpErrorResponse) {
    //console.log('handleUnprocessableEntity => ', error);
    const values = Object.values(error.error.errors) as Error[];
    return this.processError(values)

  }
  handleOtherErrors(error: HttpErrorResponse) {
    //console.log('handleBadRequest => ', error);
    if(error.error.errors){
      const values = Object.values(error.error.errors) as Error[];
      return this.processError(values)
    }else{
      var errors:Error[]=[];
      errors.push({
        code: 0,
        message:error.error 
      });
      return this.processError(errors)
    }
    
  
  }
  handleBadRequest(error: HttpErrorResponse) {
    //console.log('handleBadRequest => ', error);
    const values = Object.values(error.error.errors) as Error[];
    return this.processError(values)
  }
  handleNotFound(error: HttpErrorResponse) {
    this._router.navigate(['/404']);
    return error.message;
  }
  private handleUnauthorized = (error: HttpErrorResponse) => {
    if(this._router.url === '/account/login') {
      const values = Object.values(error.error.errors) as Error[];
      console.log(values);
      return values
    }
    else {
      this._router.navigate(['/account/login']);
      return error.message;
    }
  }
  private  processError(values: Error[]) {
    return values;
  }
}



