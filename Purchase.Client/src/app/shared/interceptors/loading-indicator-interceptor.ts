import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { finalize, first } from "rxjs/operators";
//import { finalize } from 'rxjs/operators';
import { LoadingIndicatorService } from "../services/loading-indicator-service";

@Injectable()
export class LoadingIndicatorInterceptor implements HttpInterceptor {

  constructor(private loadingIndicatorService: LoadingIndicatorService) {}

  intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // emit onStarted event before request execution
    this.loadingIndicatorService.onStarted(req);
    return next
      .handle(req)
      .pipe(finalize(()=> {
         // console.log("// emit onFinished event after request execution");
        this.loadingIndicatorService.onFinished(req);
      }));

  }

}