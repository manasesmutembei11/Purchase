import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ProductService } from '../../../services/masterdata-services/product.service'; 
import { Product, OrderItem } from '../../../models/masterdata-models/masterdata.models';
import { ProductSelectionModalComponent } from '../product-selection-modal/product-selection-modal.component';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-order-item-selection-modal',
  templateUrl: './order-item-selection-modal.component.html',
  styles: []
})
export class OrderItemSelectionModalComponent implements OnInit {
  @Output() orderItemsAdded = new EventEmitter<OrderItem[]>();

  form: FormGroup= this.fb.group({});
  products: Product[] = [];
  modalRef: NgbModalRef | null = null;

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private productService: ProductService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [Guid.create().toString()],
      label: ['', Validators.required],
      quantity: ['', Validators.required],
      unitPrice: ['', Validators.required],
      subTotal: [0],
      product: ['']
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
        id: '', 
        label: this.form.value.label,
        quantity: this.form.value.quantity,
        unitPrice: this.form.value.unitPrice,
        subTotal: this.form.value.subTotal,
        productId: this.form.value.productId
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
    if (this.modalRef) { // Check if modalRef is not null
      this.modalRef.componentInstance.selectedProducts = this.form.get('products')!.value;
      this.modalRef.componentInstance.products = this.products;
      this.modalRef.componentInstance.selectedProducts.subscribe((products: Product[]) => {
        this.form.get('products')!.setValue(products);
      });
    }
  }
  calculateSubTotal(): number {
    const quantity = this.form.get('quantity')?.value ?? 0;
    const unitPrice = this.form.get('unitPrice')?.value ?? 0;
    return quantity * unitPrice;
}
}
