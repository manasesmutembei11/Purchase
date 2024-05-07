import { Directive, HostBinding, Input, Self } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appValidityStyle]'
})
export class ValidityStyleDirective {

  @Input() submitted!: boolean;
  constructor(@Self() private ngControl: NgControl) {
    //console.log("appValidityStyle",ngControl)
  }

  @HostBinding('class.is-invalid')
  get myClass() {
   // console.log("from",this.ngControl.control?.parent);

    return (this.submitted || this.ngControl.touched) && !this.ngControl.valid

  }

}
