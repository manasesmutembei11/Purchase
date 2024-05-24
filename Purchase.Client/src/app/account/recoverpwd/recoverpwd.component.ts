import { Component, OnInit } from '@angular/core';
import { FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { BaseFormComponent } from '../../shared/base/base-form-component';
import { AuthService } from '../../core/services/auth-services/auth.service';
import { MessageBoxService } from '../../shared/services/message-box.service';
import { ForgotPassword } from '../../core/models/user-models/user';


@Component({
  selector: 'app-recoverpwd',
  templateUrl: './recoverpwd.component.html',
  styleUrls: ['./recoverpwd.component.scss']
})

/**
 * Recover Password Component
 */
export class RecoverpwdComponent extends BaseFormComponent implements OnInit {


  form!: UntypedFormGroup;
  
  
  success = '';
  loading = false;

  // set the currenr year
  year: number = new Date().getFullYear();
  // Carousel navigation arrow show
  showNavigationArrows: any;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageBox:MessageBoxService
    ) { super() }
  createForm(): FormGroup<any> {
    return this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }
  initForm() {
    throw new Error('Method not implemented.');
  }
  ngOnInit(): void {
    this.form = this.createForm()
    /**
     * Form Validation
     */

  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  /**
   * On submit form
   */
  onSubmit() {
    this.submitted = true;
    if (this.validateForm(this.form)) {
      const model = Object.assign(this.form.value) as ForgotPassword;
      console.log('model', model);

      this.authService
        .forgotPassword(model)
        .subscribe({
          next: (_) => {           
            this.messageBox.success("Please check your email to reset password.")          
            this.router.navigate(['/account/login']);
          },
          error: (errors) => {
            debugger;
            this.errors = errors;
            console.log('Error =>', this.errors);
          }
        });

    }
  }
  back() {
    
  }
}
