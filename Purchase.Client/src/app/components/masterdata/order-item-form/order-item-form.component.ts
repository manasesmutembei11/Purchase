import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OrderItemService } from '../../../services/masterdata-services/order-item.service';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { OrderItem, Product } from '../../../models/masterdata-models/masterdata.models';
import { cloneDeep } from 'lodash';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../../../services/masterdata-services/product.service'; 

@Component({
  selector: 'app-order-item-form',
  templateUrl: './order-item-form.component.html',
  styles: []
})
export class OrderItemFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  modalRef: NgbModalRef | any;
  products: Product[] = [];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    public location: Location,
    private modalService: NgbModal,
    private orderItemService: OrderItemService,
    private productService: ProductService // Inject ProductService
  ) {
    super();
  }

  ngOnInit(): void {
    this.form = this.createForm();
    this.route.params.pipe().subscribe((params) => {
      this.id = params['id'] ? params['id'] : '';
      this.editMode = params['id'] != null;
      this.pageTitle = this.editMode ? 'Edit Order Item' : 'New Order Item';
      this.breadCrumbItems = [
        { label: 'Master Data' },
        { label: 'Order Item' },
        { label: this.pageTitle, active: true },
      ];
      this.buttonText = this.editMode ? 'Update' : 'Create';
      this.initForm();
      this.loadProducts();
    });
    
  }

  createForm(): FormGroup {
    return this.fb.group({
      orderItemId: [Guid.create().toString()],
      productId: [Guid.create().toString()],
      orderId: [Guid.create().toString()],
      productName: ['', Validators.required],
      quantity: [0, Validators.required],
      subTotal: [0],
      taxRate: [0]
    });
  }

  initForm() {
    if (this.editMode) {
      this.orderItemService.findById(this.id).pipe(first())
        .subscribe({
          next: (_) => {
            this.form.patchValue(_);
          }
        });
    }
  }

  onSubmit() {
    this.submitted = true;
    if (this.validateForm(this.form)) {
      const model: OrderItem = cloneDeep(this.form.value);
      console.log("onSubmit =>", model);

      this.orderItemService.save(model).subscribe({
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

  openProductModal(content: any) {
    this.modalRef = this.modalService.open(content, { size: 'lg' });
    this.loadProducts();
  }

  loadProducts() {
    this.productService.list(1, 10, '').pipe(first())
      .subscribe({
        next: (pagedList) => {
          this.products = pagedList.data;
        }
      });
  }

  onSelectProduct(product: Product) {
    this.form.patchValue({
      productId: product.id,
      productName: product.name
    });
    this.modalRef.close(); // Close the modal after selecting the product
  }
}
