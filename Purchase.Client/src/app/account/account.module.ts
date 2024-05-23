import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { RecoverpwdComponent } from './recoverpwd/recoverpwd.component';
import { ConfirmmailComponent } from './confirmmail/confirmmail.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { SharedModule } from '../shared-components/shared.module';
import { ValidityStyleDirective } from '../services/directives/validity-style.directive';



@NgModule({
  declarations: [
   
  
    LoginComponent,
    RecoverpwdComponent,
    ConfirmmailComponent,
    ResetPasswordComponent,
    ValidityStyleDirective
    
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule
    
  ]
})
export class AccountModule { }