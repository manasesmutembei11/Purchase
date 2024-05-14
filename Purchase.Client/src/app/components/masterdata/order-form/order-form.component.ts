import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { cloneDeep } from 'lodash';
import { Customer, Order, OrderItem } from '../../../models/masterdata-models/masterdata.models';
import { OrderService } from '../../../services/masterdata-services/order.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { OrderItemSelectionModalComponent } from '../../modals/order-item-selection-modal/order-item-selection-modal.component';
import { CustomerSelectionModalComponent } from '../../modals/customer-selection-modal/customer-selection-modal.component';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styles: []
})
export class OrderFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  modalRef: NgbModalRef | null = null;
  orderItems: OrderItem[] = [];
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private orderService: OrderService,
    private modalService: NgbModal,
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
      id: [Guid.create().toString()],
      customerId: [Guid.create().toString()],
      customerName: ['', Validators.required],
      orderDate: [Date.now()],
      orderItems: [[]],
      total: [0]
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

  openOrderItemModal(): void {
    this.modalRef = this.modalService.open(OrderItemSelectionModalComponent, { size: 'lg' });
    if (this.modalRef) { // Check if modalRef is not null
      this.modalRef.componentInstance.selectedOrderItems = this.form.get('orderItems')!.value;
      this.modalRef.componentInstance.orderItems = this.orderItems;
      this.modalRef.componentInstance.selectedOrderItems.subscribe((orderItems: OrderItem[]) => {
        this.form.get('orderItems')!.setValue(orderItems);
      });
    }
  }

  openCustomerModal() {
    const modalRef = this.modalService.open(CustomerSelectionModalComponent, { size: 'lg' });
    modalRef.componentInstance.selectedCustomer.subscribe((customer: Customer) => {
      this.form.patchValue({
        customerName: customer.firstName,
        customerId: customer.id
      });
    });
  }


}
