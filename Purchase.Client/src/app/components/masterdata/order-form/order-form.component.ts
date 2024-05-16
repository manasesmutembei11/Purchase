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
      orderDate: [Date],
      orderItems: [[]],
      customerName: [''],
      customerPhone: [''],
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
        error: (error) => {
          this.error = error;
          console.log('Error =>', this.error);
        },
      });
    }
  }
  back(): void {
    this.location.back();
  }

  removeOrderItem(index: number): void {
    this.orderItems.splice(index, 1); // Remove order item at the specified index
  }


  openOrderItemModal(): void {
    const modalRef: NgbModalRef = this.modalService.open(OrderItemSelectionModalComponent, { size: 'lg' });
    modalRef.componentInstance.orderItemsAdded.subscribe((orderItems: OrderItem[]) => {
      // Handle newly added order items
      console.log('Newly added order items:', orderItems);
      // Add order items to the form or perform other actions
      this.orderItems.push(...orderItems); // Add to existing order items
    });
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
