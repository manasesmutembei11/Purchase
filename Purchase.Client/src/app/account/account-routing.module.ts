import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RecoverpwdComponent } from './recoverpwd/recoverpwd.component';
import { ConfirmmailComponent } from './confirmmail/confirmmail.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'recoverpw', component: RecoverpwdComponent },
  { path: 'confirm-email', component: ConfirmmailComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
