import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Customer } from '../../models/masterdata-models/masterdata.models';
import { CustomerService } from '../../services/masterdata-services/customer.service';
import { BasePagedListComponent } from '../../../shared/base/base-paged-list-component';
import { first } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Params } from "@angular/router";
import { CommonModule } from '@angular/common';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-selection-modal',
  templateUrl: './customer-selection-modal.component.html',
  standalone: true,
  imports: [
    CommonModule,
    NgbModalModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
  ],
})
export class CustomerSelectionModalComponent extends BasePagedListComponent implements OnInit {
  customers: Customer[] = [];
  @Output() selectedCustomer = new EventEmitter<Customer>();

  constructor(
    public activeModal: NgbActiveModal,
    private customerService: CustomerService,
    public modalService: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    // Fetch categories here
    this.customerService.list(this.page, this.pageSize, this.search).subscribe((customers) => {
      this.customers = customers.data;
    });
  }

  selectCustomer(customer: Customer): void {
    this.selectedCustomer.emit(customer);
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
