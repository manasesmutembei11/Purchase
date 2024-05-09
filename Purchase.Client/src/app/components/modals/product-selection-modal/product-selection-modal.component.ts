import { Component, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../services/masterdata-services/product.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Product } from '../../../models/masterdata-models/masterdata.models';

@Component({
  selector: 'app-product-selection-modal',
  templateUrl: './product-selection-modal.component.html',
  styles: []
})
export class ProductSelectionModalComponent {
  @Output() productSelected = new EventEmitter<Product>();

  products: Product[] = [];
  

  constructor(public modalRef: NgbModalRef,private modalService: NgbModal, private productService: ProductService) { }

  open(content: any) {
    this.loadProducts(); // Load products when the modal opens
    this.modalRef = this.modalService.open(content, { size: 'lg' });
  }

  loadProducts() {
    this.productService.list(1, 10, '').subscribe((pagedList) => {
      this.products = pagedList.data;
    });
  }

  selectProduct(product: Product) {
    this.productSelected.emit(product);
    this.modalRef.close(); // Close the modal after selecting the product
  }

}
