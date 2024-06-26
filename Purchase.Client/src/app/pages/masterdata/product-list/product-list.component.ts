import { Component, OnInit } from '@angular/core';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { Product } from '../../../core/models/masterdata-models/masterdata.models';
import { ProductService } from '../../../core/services/masterdata-services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styles: []
})
export class ProductListComponent extends BasePagedListComponent implements OnInit {
  items: Product[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private productService: ProductService
  ) { super() }

  ngOnInit(): void {
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "Product" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    this.productService.list(this.page, this.pageSize, this.search)
      .pipe(first())
      .subscribe({
        next: (_) => {
          this.items = _.data;
          this.totalCount = _.metaData.totalCount;
        }
      });
    const params:Params = {
      page: this.page,
    }
    this.router.navigate(["."], { relativeTo: this.route, queryParams: params, queryParamsHandling: 'merge' });

  }


  addToOrder(productId: string): void {
    const product = this.items.find(p => p.id === productId);
    if (product && product.quantity > 0) {
      this.productService.decreaseProductQuantity(productId, 1).subscribe(
        () => {
          product.quantity--; // Update locally
        },
        (error) => {
          console.error('Error updating product quantity:', error);
        }
      );
    } else {
      console.log('Product not available');
    }
  }

}
