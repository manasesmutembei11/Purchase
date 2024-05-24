import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { NgxErrorsModule } from '@ngspot/ngx-errors';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { RecoverpwdComponent } from './recoverpwd/recoverpwd.component';
import { ConfirmmailComponent } from './confirmmail/confirmmail.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';



@NgModule({
  declarations: [
   
  
    LoginComponent,
    RecoverpwdComponent,
    ConfirmmailComponent,
    ResetPasswordComponent,
    
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    SharedModule,
    NgxErrorsModule,
    
  ]
})
export class AccountModule { }
