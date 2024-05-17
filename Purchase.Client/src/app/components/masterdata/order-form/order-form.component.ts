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
import { ProductService } from '../../../services/masterdata-services/product.service';
import { OrderItemService } from '../../../services/masterdata-services/order-item.service';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styles: []
})
export class OrderFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  modalRef: NgbModalRef | null = null;
  orderItems: OrderItem[] = [];
  total: number= 0;
  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private orderService: OrderService,
    private modalService: NgbModal,
    private productService: ProductService,
    private orderItemService: OrderItemService,
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
      productId: [''],
      orderDate: [Date],
      orderItems: [this.orderItems],
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
          next: (order) => {
            this.form.patchValue(order);
            this.orderItems = order.orderItems;
            this.calculateTotal();
          }
        });
    }
  }
  onSubmit() {
    this.submitted = true;
    if (this.validateForm(this.form)) {
      this.saveOrder();
    }
  }

  saveOrder() {
    const model: Order = cloneDeep(this.form.value);
    this.orderService.save(model).subscribe({
      next: (savedOrder) => {
        console.log('Order saved successfully', savedOrder);
        this.saveOrderItems(savedOrder.id);
      },
      error: (error) => {
        this.error = error;
        console.log('Error =>', this.error);
      }
    });
  }


  saveOrderItems(orderId: string) {
    const orderItemsToSave = this.orderItems.map(item => ({
      ...item,
      orderId: orderId
    }));
    orderItemsToSave.forEach(orderItem => {
      this.orderItemService.save(orderItem).subscribe({
        next: (response) => {
          if (response) {
            console.log('Order item saved successfully', orderItem);
          } else {
            console.error('Failed to save order item');
          }
        },
        error: (error) => {
          console.error('Error =>', error);
        },
      });
    });
    this.location.back();
  }

  back(): void {
    this.location.back();
  }

  removeOrderItem(index: number): void {
    const removedItem = this.orderItems.splice(index, 1)[0];
    this.updateProductQuantity(removedItem.productId, removedItem.quantity);
    this.calculateTotal();
  }


  openOrderItemModal(): void {
    const modalRef: NgbModalRef = this.modalService.open(OrderItemSelectionModalComponent, { size: 'lg' });
    modalRef.componentInstance.orderId = this.form.get('id')!.value;
    modalRef.componentInstance.orderItemsAdded.subscribe((orderItems: OrderItem[]) => {
      orderItems.forEach(item => {
        item.productId = item.productId ?? '';
        item.orderId = this.form.get('id')!.value; 

        this.updateProductQuantity(item.productId, item.quantity);
      });

      this.orderItems.push(...orderItems);
      this.calculateTotal();
      const itemNames = orderItems.map(item => item.label);
      this.form.patchValue({ orderItems: this.orderItems, ItemNames: itemNames });
    });
  }
  

  openCustomerModal() {
    const modalRef = this.modalService.open(CustomerSelectionModalComponent, { size: 'lg' });
    modalRef.componentInstance.selectedCustomer.subscribe((customer: Customer) => {
      this.form.patchValue({
        customerName: customer.firstName,
        customerPhone: customer.phone,
        customerId: customer.id
      });
    });
  }

  calculateTotal(): void {
    this.total = this.orderItems.reduce((acc, item) => acc + item.subTotal, 0);
    this.form.patchValue({ total: this.total });
  }

  updateProductQuantity(productId: string, quantityChange: number): void {
    this.productService.findById(productId).subscribe((product) => {
      if (product) {
        const newQuantity = Math.max(product.quantity - quantityChange, 0);
        product.quantity = newQuantity;
        this.productService.save(product).subscribe(() => {
          console.log(`Product quantity updated: ${product.quantity}`);
        });
      }
    });
  }
 
  
  
}



