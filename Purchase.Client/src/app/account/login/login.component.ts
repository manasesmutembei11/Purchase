import { Component } from '@angular/core';
import { FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormComponent } from '../../shared/base/base-form-component';
import { AuthService } from '../../core/services/auth-services/auth.service';
import { LAYOUT_MODE } from '../../layouts/layouts.model';
import { AuthenticationDto } from '../../core/models/auth-models/authentication-dto';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent extends BaseFormComponent {
  
  // set the currenr year
  year: number = new Date().getFullYear();
  // Carousel navigation arrow show
  showNavigationArrows: any;
  form!: UntypedFormGroup;
 
  returnUrl!: string;
  layout_mode!: string;
  fieldTextType!: boolean;

  constructor(private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService
  ) {
    super()
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }
  createForm(): FormGroup<any> {
    return this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  initForm() {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.layout_mode = LAYOUT_MODE
    if (this.layout_mode === 'dark') {
      document.body.setAttribute("data-layout-mode", "dark");
    }
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    document.body.setAttribute('data-layout', 'vertical');
    //Validation Set
   this.form=this.createForm();
  
   
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  /**
   * Form submit
   */
  onSubmit() {
    this.submitted = true;
   
    if (this.validateForm(this.form)) {
      const model = Object.assign(this.form.value) as AuthenticationDto;      
      this.authenticationService.loginUser(model).subscribe({
        next: (_) => {
          this.router.navigate([this.returnUrl]);
        },
        error: (errors) => {
          this.errors = errors;
          console.log('Error =>', this.errors);
        },
      });
    }
    
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
  back() {
  
  }
}
