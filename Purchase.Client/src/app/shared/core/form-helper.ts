import { AbstractControl, FormControl, FormGroup } from "@angular/forms";

export class FormHelper{
    static  validateAllFormFields(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach((field) => {
          const control = formGroup.get(field);
    
          if (control instanceof FormControl) {
            if (!control.valid) {
              console.log(`Control => ${this.getControlName(control)}`,control.errors);
            }
            control.markAsTouched({ onlySelf: true });
          } else if (control instanceof FormGroup) {
            this.validateAllFormFields(control);
          }
        });
      }
     private static getControlName(c: AbstractControl): string | null {
        const formGroup = c.parent!.controls;
        //@ts-ignore
        return Object.keys(formGroup).find((name) => c === formGroup[name]) || null;
      }
      static validateForm(formGroup: FormGroup): boolean {
        if (formGroup.valid) {
          return true;
        } else {
          FormHelper.validateAllFormFields(formGroup)          
          return false;
        }
      }
}