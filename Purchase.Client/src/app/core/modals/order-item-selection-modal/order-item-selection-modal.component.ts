import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../../services/masterdata-services/product.service';
import { Product, Order, OrderItem } from '../../models/masterdata-models/masterdata.models';
import { ProductSelectionModalComponent } from '../product-selection-modal/product-selection-modal.component';
import { Guid } from 'guid-typescript';
import { OrderItemService } from '../../services/masterdata-services/order-item.service';
import { CommonModule } from '@angular/common';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-order-item-selection-modal',
  templateUrl: './order-item-selection-modal.component.html',
  standalone: true,
  imports: [
    CommonModule,
    NgbModalModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
  ],
})
export class OrderItemSelectionModalComponent implements OnInit {
  @Output() orderItemsAdded = new EventEmitter<OrderItem[]>();
  @Input() orderId!: string;

  form: FormGroup= this.fb.group({});
  products: Product[] = [];
  modalRef: NgbModalRef | null = null;

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private productService: ProductService,
    private modalService: NgbModal,
    private orderItemService: OrderItemService
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [Guid.create().toString()],
      label: ['', Validators.required],
      quantity: ['', Validators.required],
      unitPrice: [''],
      subTotal: [0],
      productName: [''],
      productId: [''],
      orderId: [this.orderId]
    });

    this.loadProducts();
    this.calculateSubTotal();
  }

  loadProducts(): void {
    this.productService.list(1, 10, '').subscribe({
      next: (pagedList) => {
        this.products = pagedList.data;
      }
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const orderItem: OrderItem = {
        id: Guid.create().toString(), 
        label: this.form.value.label,
        quantity: this.form.value.quantity,
        unitPrice: this.form.value.unitPrice,
        subTotal: this.form.value.subTotal,
        productName: this.form.value.productName,
        productId: this.form.value.productId,
        orderId: this.orderId

      };
      this.orderItemsAdded.emit([orderItem]);
      this.activeModal.close();
    }
  }

  closeModal(): void {
    this.activeModal.dismiss('Cross click');
  }


  openProductModal(): void {
    this.modalRef = this.modalService.open(ProductSelectionModalComponent, { size: 'lg' });
    this.modalRef.componentInstance.selectedProductsChange.subscribe((products: Product[]) => {
      if (products.length > 0) {
        const selectedProduct = products[0]; 
        const quantity = this.form.get('quantity')!.value;

        if (quantity > selectedProduct.quantity) {
          console.error("Requested quantity exceeds available quantity for the selected product");
        } else {
          const subTotal = selectedProduct.price * quantity;
          this.form.patchValue({
            productName: selectedProduct.name,
            productId: selectedProduct.id,
            unitPrice: selectedProduct.price,
            subTotal: subTotal
          });
        }
      }
    });
  }
  
  calculateSubTotal(): void {
    const quantity = this.form.get('quantity')?.value ?? 0;
    const unitPrice = this.form.get('unitPrice')?.value ?? 0;
    const subTotal = quantity * unitPrice;
    this.form.patchValue({ subTotal: subTotal });
  }
}

