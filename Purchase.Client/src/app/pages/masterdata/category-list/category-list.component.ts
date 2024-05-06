import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { first } from 'rxjs';
import { BasePagedListComponent } from '../../../core/models/shared/base-paged-list-component';
import { Category } from '../../../core/models/masterdata/category.model';
import { CategoryService } from '../../../core/services/masterdata/category.service';


@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styles: [
  ]
})
export class CategoryListComponent extends BasePagedListComponent implements OnInit {
  items: Category[] = [];
  constructor(
    protected router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) { super() }

  ngOnInit(): void {
    
    this.pageTitle = "List"
    this.breadCrumbItems = [
      { label: "Master Data" },
      { label: "Category" },
      { label: this.pageTitle, active: true }
    ]
    this.route.queryParams.pipe(first()).subscribe({next:(_) => this.fetchPagingParams(_)});
    this.loadItems()
  }
 
 

  loadItems() {
    
    
    this.categoryService.list(this.page, this.pageSize, this.search)
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
