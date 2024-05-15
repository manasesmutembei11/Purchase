import { Component, OnInit } from '@angular/core';
import { BasePagedListComponent } from '../../base/base-paged-list-component';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { OrderItem } from '../../../models/masterdata-models/masterdata.models';
import { OrderItemService } from '../../../services/masterdata-services/order-item.service';
import { ProductService } from '../../../services/masterdata-services/product.service';

@Component({
  selector: 'app-order-item-list',
  templateUrl: './order-item-list.component.html',
  styles: []
})
export class OrderItemListComponent extends BasePagedListComponent implements OnInit  {
  items: OrderItem[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private orderItemService: OrderItemService,
    private productService: ProductService
  ) { super() }

  ngOnInit(): void {
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "OrderItem" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    
    
    this.orderItemService.list(this.page, this.pageSize, this.search)
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


}
