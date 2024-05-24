
import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';


import Swal from 'sweetalert2';
import { BaseFormComponent } from '../../shared/base/base-form-component';
import { AuthService } from '../../core/services/auth-services/auth.service';
import { PasswordConfirmationValidatorService } from '../../shared/custom-validators/password-confirmation-validator.service';
import { CustomValidators } from '../../shared/custom-validators/custom-validators';
import { ResetPassword } from '../../core/models/user-models/user';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent extends BaseFormComponent implements OnInit {
  back() {
    
  }

  form: FormGroup= this.fb.group({});
  year: number = new Date().getFullYear();
  token: any;
  email: any;
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router:Router,
    private authService:AuthService,
    private _passConfValidator: PasswordConfirmationValidatorService,
    public location: Location
    ) { super()}

  ngOnInit(): void {
    this.form = this.createForm();
    this.form.controls['confirmPassword'].setValidators([Validators.required ,this._passConfValidator.validateConfirmPassword(this.form.controls['password'])]);
    this.token = this.route.snapshot.queryParams['token'];
    this.email = this.route.snapshot.queryParams['email'];

  }

  createForm(): FormGroup {
    const f = this.fb.group({
      password: new FormControl('', [Validators.required,CustomValidators.password]),
      confirmPassword: new FormControl(''),
    });
    return f;
  }
  initForm() {

  }
  onSubmit(){

    this.submitted=true;
    if (this.validateForm(this.form)) {
      const model = Object.assign(this.form.value) as ResetPassword;
      console.log('model', model);
      model.email=this.email;
      model.token=this.token;

      this.authService
        .resetPassword(model)
        .subscribe({
          next: (_) => {
            Swal.fire('Success!','Password Reset successfully','success')
            this.router.navigate(['/account/login']);
          },
          error: (errors) => {
            this.errors = errors;
            console.log('Error =>', this.errors);
          }
        });

    }
  }

}
