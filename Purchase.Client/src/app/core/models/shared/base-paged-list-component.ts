
import { Component, OnDestroy } from "@angular/core";
import { Subject } from "rxjs";
import { BaseComponent } from "./base-component";
import { Params } from "@angular/router";


@Component({
  selector: 'app-base-paged-list',
  template: ` <div>base works!!</div> `,
})
export abstract class BasePagedListComponent extends BaseComponent implements OnDestroy {
  protected unsubscribe: Subject<Object | any>;
  search: string;
  page: number = 1;
  pageSize: number = 10;
  totalCount: number = 0;
  maxSize: number = 10;
  constructor() {
    super();
   
    this.unsubscribe = new Subject<Object>();
    this.search = '';
  }
  ngOnDestroy(): void {
    this.destroy();
  }

  protected destroy(): void {
    
    this.unsubscribe.next(null);
    this.unsubscribe.complete();
  }
  abstract loadItems(): any;


  onSearch(): void {
    

    this.loadItems()

  }
  onReset(): void {
   
    this.search = '';
    this.loadItems()
  }

  public pageChanged(page: any): void {      
   
   this.page = page;

    this.loadItems();
  }

  fetchPagingParams(params: Params): void {
    const page: number = parseInt(params['page']);
    if (!Number.isNaN(page)) {
      this.page = page
    }
  }

}
