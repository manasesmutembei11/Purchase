import { Component, OnInit } from '@angular/core';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { Customer } from '../../../core/models/masterdata-models/masterdata.models';
import { CustomerService } from '../../../core/services/masterdata-services/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styles: []
})
export class CustomerListComponent extends BasePagedListComponent implements OnInit {
  items: Customer[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private customerService: CustomerService
  ) { super() }

  ngOnInit(): void {
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "Customer" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    
    
    this.customerService.list(this.page, this.pageSize, this.search)
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
