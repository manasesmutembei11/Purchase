import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../shared/base/base-form-component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OrderItemService } from '../../../core/services/masterdata-services/order-item.service';
import { Guid } from 'guid-typescript';
import { first } from 'rxjs';
import { OrderItem, Product } from '../../../core/models/masterdata-models/masterdata.models';
import { cloneDeep } from 'lodash';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../../../core/services/masterdata-services/product.service';
import { ProductSelectionModalComponent } from '../../../core/modals/product-selection-modal/product-selection-modal.component';

@Component({
  selector: 'app-order-item-form',
  templateUrl: './order-item-form.component.html',
  styles: []
})
export class OrderItemFormComponent extends BaseFormComponent implements OnInit {
  form: FormGroup = this.fb.group({});
  modalRef: NgbModalRef | null = null;
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
      id: [Guid.create().toString()],
      product: [''],
      label: [''],
      unitPrice: [0],
      subTotal: [0],
      quantity: [0],
      orderId: [''],
      productId: ['']
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


  openProductModal(): void {
    this.modalRef = this.modalService.open(ProductSelectionModalComponent, { size: 'lg' });
    if (this.modalRef) { // Check if modalRef is not null
      this.modalRef.componentInstance.selectedProducts = this.form.get('products')!.value;
      this.modalRef.componentInstance.products = this.products;
      this.modalRef.componentInstance.selectedProducts.subscribe((products: Product[]) => {
        // Check available quantity before adding to order item
        const invalidProducts = products.filter(product => {
          const requestedQuantity = this.form.get('quantity')!.value;
          return product.quantity < requestedQuantity;
        });
  
        if (invalidProducts.length > 0) {
          // Handle invalid products, such as showing an error message
          console.error("Requested quantity exceeds available quantity for some products");
        } else {
          // Calculate subtotal and set it in the form
          const price = products[0].price; // Assuming only one product is selected
          const quantity = this.form.get('quantity')!.value;
          const subTotal = price * quantity;
          this.form.patchValue({ subTotal: subTotal });
          
          // Set selected products in the form
          this.form.get('products')!.setValue(products);
        }
      });
    }
  }
  
  

  loadProducts() {
    this.productService.list(1, 10, '').pipe(first())
      .subscribe({
        next: (pagedList) => {
          this.products = pagedList.data;
        }
      });
  }
}
