import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';

@Injectable()
export class CustomValidators {
    static ageRangeValidator(min: number, max: number): ValidatorFn {
        return (control: AbstractControl): { [key: string]: boolean } | null => {
            if (control.value !== undefined && (isNaN(control.value) || control.value < min || control.value > max)) {
                return { 'ageRange': true };
            }
            return null;
        };
    }

    static phone(control: AbstractControl): ValidationErrors | null  {
        const regex = /^\d{8,15}$/;
        const test = regex.test(control.value);
        if (!test) {
            return { 'phone': true };
        }
        return  null;
    }
    static percent(control: AbstractControl): ValidationErrors | null  {
        const regex = /^(?:\d{1,2}(?:\.\d{1,2})?|100(?:\.0?0)?)$/;
        const test = regex.test(control.value);
        if (!test) {
            return { 'percent': true };
        }
        return  null;
    }
    static amount(control: AbstractControl): ValidationErrors | null  {
        const regex = /^(?=.*?\d)^\$?(([1-9]\d{0,2}(,\d{3})*)|\d+)?(\.\d{1,2})?$/;
        const test = regex.test(control.value);
        if (!test) {
            return { 'amount': true };
        }
        return  null;
    }
    static number(control: AbstractControl): ValidationErrors | null  {
        const regex = /^(?:\d{1,3}(?:\.\d{1,2})?|1000(?:\.0?0)?)$/;
        const test = regex.test(control.value);
        if (!test) {
            return { 'number': true };
        }
        return  null;
    }
    static numeric(control: AbstractControl) {
        let val = control.value;
        if (val === null || val === '') return null;
        if (!val.toString().match(/^[0-9]+(\.?[0-9]+)?$/)) return { 'numeric': true };
        return null;
      }
    static password(control: AbstractControl): ValidationErrors | null  {
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
        const test = regex.test(control.value);
        if (!test) {
            return { 'password': true };
        }
        return  null;
    }
    static allowZero (control: AbstractControl): ValidationErrors | null  {
        const value = control.value;
        if (value || value === 0 || value === '0') {
          return null; // Valid
        } else {
          return { 'allowZero': { value: control.value } }; // Invalid
        }
      };



}
