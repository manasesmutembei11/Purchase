import { FormGroup, ValidatorFn } from "@angular/forms"

export function AtLeastOneCheckboxCheckedValidator(controlName: string): ValidatorFn | any {
    let minRequired=1
    return function validate(formGroup: FormGroup) {
        let checked = 0  
        Object.keys(formGroup.controls).forEach(key => {
            const control = formGroup.controls[key]?.get(controlName)            
            //const controlParent = formGroup?.parent?.parent
            //console.log(`controlParent => Key ${key} `, controlParent?.value.enableEmail);
            if (control && control.value) {
                checked++
            }
        })

        if (checked < minRequired) {
            return {
                requireCheckboxToBeChecked: true,
            }
        }
        return null
    }
}