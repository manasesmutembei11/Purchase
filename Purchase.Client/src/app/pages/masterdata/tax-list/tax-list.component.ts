import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { KeyValue } from '@angular/common';

import { first } from 'rxjs';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { Tax } from '../../../core/models/masterdata-models/masterdata.models';
import { TaxService } from '../../../core/services/masterdata-services/tax.service';

@Component({
  selector: 'app-tax-list',
  templateUrl: './tax-list.component.html',
  styles: []
})
export class TaxListComponent extends BasePagedListComponent implements OnInit {
  items: Tax[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private taxService: TaxService
  ) { super() }

  ngOnInit(): void {
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "Tax" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    
    
    this.taxService.list(this.page, this.pageSize, this.search)
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
