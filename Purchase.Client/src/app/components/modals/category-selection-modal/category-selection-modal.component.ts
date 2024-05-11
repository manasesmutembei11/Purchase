import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Category } from '../../../models/masterdata-models/masterdata.models';
import { CategoryService } from '../../../services/masterdata-services/category.service';
import { BasePagedListComponent } from '../../base/base-paged-list-component';
import { first } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Params } from "@angular/router";

@Component({
  selector: 'app-category-selection-modal',
  templateUrl: './category-selection-modal.component.html',
  styles: []
})
export class CategorySelectionModalComponent extends BasePagedListComponent implements OnInit {
  categories: Category[] = [];
  @Output() selectedCategory = new EventEmitter<Category>();

  constructor(
    public activeModal: NgbActiveModal,
    private categoryService: CategoryService,
    public modalService: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    // Fetch categories here
    this.categoryService.list(this.page, this.pageSize, this.search).subscribe((categories) => {
      this.categories = categories.data;
    });
  }

  selectCategory(category: Category): void {
    this.selectedCategory.emit(category);
    this.modalService.dismissAll();
  }

  close() {
    this.activeModal.dismiss('Cross click');
  }

  override fetchPagingParams(params: Params): void {
    super.fetchPagingParams(params);
    this.loadItems();
  }

}
