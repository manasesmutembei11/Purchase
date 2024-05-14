import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { OrderItemService } from '../../../services/masterdata-services/order-item.service';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { OrderItem } from '../../../models/masterdata-models/masterdata.models';
import { BasePagedListComponent } from '../../base/base-paged-list-component';
import { Params } from '@angular/router';

@Component({
  selector: 'app-order-item-selection-modal',
  templateUrl: './order-item-selection-modal.component.html',
  styles: []
})
export class OrderItemSelectionModalComponent extends BasePagedListComponent implements OnInit {
  orderItems: OrderItem[] = [];
  @Output() selectedOrderItemsChange = new EventEmitter<OrderItem[]>(); 
  selectedOrderItemIds: string[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private orderItemService: OrderItemService,
    public modalService: NgbModal
  ) {
    super();
  }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    // Fetch categories here
    this.orderItemService.list(this.page, this.pageSize, this.search).subscribe((orderItems) => {
      this.orderItems = orderItems.data;
    });
  }

  selectOrderItem(orderItem: OrderItem): void {
    if (!this.selectedOrderItemIds) this.selectedOrderItemIds = []; // Null check
    const index = this.selectedOrderItemIds.indexOf(orderItem.id);
    if (index === -1) {
      this.selectedOrderItemIds.push(orderItem.id);
    } else {
      this.selectedOrderItemIds.splice(index, 1);
    }
  }

  saveOrderItems(): void {
    const selectedOrderItems = this.orderItems.filter(orderItem => this.selectedOrderItemIds.includes(orderItem.id));
    this.selectedOrderItemsChange.emit(selectedOrderItems); // Emit through selectedProductsChange
    this.activeModal.close();
  }

  close() {
    this.activeModal.dismiss('Cross click');
  }

  override fetchPagingParams(params: Params): void {
    super.fetchPagingParams(params);
    this.loadItems();
  }

}
