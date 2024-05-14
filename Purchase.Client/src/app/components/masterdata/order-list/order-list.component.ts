import { Component, OnInit } from '@angular/core';
import { BasePagedListComponent } from '../../base/base-paged-list-component';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { Customer, Order } from '../../../models/masterdata-models/masterdata.models';
import { OrderService } from '../../../services/masterdata-services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styles: []
})
export class OrderListComponent extends BasePagedListComponent implements OnInit {
  items: Order[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private orderService: OrderService
  ) { super() }

  ngOnInit(): void {
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "Order" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    
    
    this.orderService.list(this.page, this.pageSize, this.search)
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
