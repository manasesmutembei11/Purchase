import { AbstractControl, ValidationErrors, Validators } from "@angular/forms";
import { PhoneNumberUtil } from "google-libphonenumber";

const phoneNumberUtil = PhoneNumberUtil.getInstance();


export const phoneNumberValidator = (control: AbstractControl): ValidationErrors | null => {
    let validNumber,required = false;
    try {
       required = control.hasValidator(Validators.required)
      const phoneNumber = phoneNumberUtil.parseAndKeepRawInput(
        control.value.phoneNumber, control.value.iso2
      );
      validNumber = phoneNumberUtil.isValidNumber(phoneNumber);
    } catch (e) { }
       //console.log("working");
       var value =control.value?.phoneNumber
       if(!value && !required ){
        return null;
       }
     return  validNumber ? null : { phoneNumber: true }
  }
