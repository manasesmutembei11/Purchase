import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { ProductService } from '../../services/masterdata-services/product.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Product } from '../../models/masterdata-models/masterdata.models';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { Params } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-selection-modal',
  templateUrl: './product-selection-modal.component.html',
  standalone: true,
  imports: [
    CommonModule,
    NgbModalModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
  ],
})
export class ProductSelectionModalComponent extends BasePagedListComponent implements OnInit {
  products: Product[] = [];
  @Output() selectedProductsChange = new EventEmitter<Product[]>(); // Rename event emitter
  selectedProductIds: string[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private productService: ProductService,
    public modalService: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    // Fetch categories here
    this.productService.list(this.page, this.pageSize, this.search).subscribe((products) => {
      this.products = products.data;
    });
  }

  selectProduct(product: Product): void {
    if (!this.selectedProductIds) this.selectedProductIds = []; // Null check
    const index = this.selectedProductIds.indexOf(product.id);
    if (index === -1) {
      this.selectedProductIds.push(product.id);
    } else {
      this.selectedProductIds.splice(index, 1);
    }
  }

  saveProducts(): void {
    const selectedProducts = this.products.filter(product => this.selectedProductIds.includes(product.id));
    this.selectedProductsChange.emit(selectedProducts); // Emit through selectedProductsChange
    this.activeModal.close();
  }

  close() {
    this.activeModal.dismiss('Cross click');
  }

  override fetchPagingParams(params: Params): void {
    super.fetchPagingParams(params);
    this.loadItems();
  }

}
