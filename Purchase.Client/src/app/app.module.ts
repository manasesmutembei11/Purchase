import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LayoutsModule } from './layouts/layouts.module';
import { AppRoutingModule } from './app-routing.module';
import { JwtConfig, JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { AppJwtInterceptor } from './shared/interceptors/app-jwt-interceptor';
import { LoadingIndicatorService } from './shared/services/loading-indicator-service';
import { LoadingIndicatorInterceptor } from './shared/interceptors/loading-indicator-interceptor';
import { NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { CustomDateParserFormatter } from './shared/core/custom-date-parser-formatter';

export function createTranslateLoader(http: HttpClient): any {
  return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}
export function tokenGetter() {
  return localStorage.getItem("jwt");
}
function jwtConfigOptionGetter(): JwtConfig {
  return {
    tokenGetter: tokenGetter,
    allowedDomains: [],
    disallowedRoutes: [],
  };
}


@NgModule({
  declarations: [
    AppComponent,
   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en',
      loader: {
        provide: TranslateLoader,
        useFactory: (createTranslateLoader),
        deps: [HttpClient]
      }
    }),     
    LayoutsModule,
    AppRoutingModule,
 
    
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: AppJwtInterceptor, multi: true },
    LoadingIndicatorService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingIndicatorInterceptor,
      multi: true,
      deps: [LoadingIndicatorService],
    },
    {
      provide: JWT_OPTIONS,
      useValue: jwtConfigOptionGetter(),
    },
    JwtHelperService,
    {
      provide: NgbDateParserFormatter,
      useValue: new CustomDateParserFormatter("dd-MMM-yyyy"), // <== format!
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
