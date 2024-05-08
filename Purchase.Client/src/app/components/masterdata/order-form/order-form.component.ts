import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { cloneDeep } from 'lodash';
import { Order } from '../../../models/masterdata-models/masterdata.models';
import { OrderService } from '../../../services/masterdata-services/order.service';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styles: []
})
export class OrderFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private orderService: OrderService
  ) {
    super();
  }
  ngOnInit(): void {
    this.form = this.createForm();
    this.route.params.pipe().subscribe((params) => {
      this.id = params['id'] ? params['id'] : '';
      this.editMode = params['id'] != null;
      this.pageTitle = this.editMode ? 'Edit Order' : 'New Order';
      this.breadCrumbItems = [
        { label: 'Master Data' },
        { label: 'Order' },
        { label: this.pageTitle, active: true },
      ];
      this.buttonText = this.editMode ? 'Update' : 'Create';
      this.initForm();
    });
  }
  createForm(): FormGroup {
    const f = this.fb.group({
      code: ['', [Validators.required]],
      name: ['', Validators.required],
      price: [0, Validators.required],
      hasTax: [0],
      taxRate: [0, Validators.required],
      quantity: [0, Validators.required],
      description: ['', Validators.required],
      productId: [Guid.create().toString()],
    });
    return f;
  }
  initForm() {
    if (this.editMode) {
      this.orderService.findById(this.id).pipe(first())
        .subscribe({
          next: (_) => {
            this.form.patchValue(_);
          }
        })

    }
  }
  onSubmit() {
    this.submitted = true;
    if (this.validateForm(this.form)) {
      const model: Order = cloneDeep(this.form.value)
      console.log("onSubmit =>",model);
      

      this.orderService.save(model).subscribe({
        next: (_) => {
          this.location.back();
        },
        error: (errors) => {
          this.errors = errors;
          console.log('Error =>', this.errors);
        },
      });
    }
  }
  back(): void {
    this.location.back();
  }

}
