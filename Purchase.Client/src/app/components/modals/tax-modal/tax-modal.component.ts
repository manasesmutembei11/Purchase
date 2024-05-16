import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {  Tax } from '../../../models/masterdata-models/masterdata.models';
import { TaxService } from '../../../services/masterdata-services/tax.service';
import { BasePagedListComponent } from '../../base/base-paged-list-component';
import { first } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Params } from "@angular/router";

@Component({
  selector: 'app-tax-modal',
  templateUrl: './tax-modal.component.html',
  styles: []
})
export class TaxModalComponent extends BasePagedListComponent implements OnInit {
  taxes: Tax[] = [];
  @Output() selectedTax = new EventEmitter<Tax>();

  constructor(
    public activeModal: NgbActiveModal,
    private taxService: TaxService,
    public modalService: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    // Fetch categories here
    this.taxService.list(this.page, this.pageSize, this.search).subscribe((taxes) => {
      this.taxes = taxes.data;
    });
  }

  selectTax(tax: Tax): void {
    this.selectedTax.emit(tax);
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
