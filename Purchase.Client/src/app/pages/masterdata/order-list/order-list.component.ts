import { Component, OnInit } from '@angular/core';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { Customer, Order } from '../../../core/models/masterdata-models/masterdata.models';
import { OrderService } from '../../../core/services/masterdata-services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styles: []
})
export class OrderListComponent extends BasePagedListComponent implements OnInit {
  items: Order[] = [];
  orderId: string = '';
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
    this.route.params.subscribe(params => {
      this.orderId = params['id'];
    });
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

  cancelOrder(): void {
    this.orderService.cancelOrder(this.orderId).subscribe(
      (response) => {
        console.log('Order canceled successfully:', response);
        // Handle any UI updates or notifications
      },
      (error) => {
        console.error('Error canceling order:', error);
        // Handle error responses
      }
    );
  }



}
