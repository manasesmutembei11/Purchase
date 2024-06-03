import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterdataRoutingModule } from './masterdata-routing.module';
import { NgbModalModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxErrorsModule } from '@ngspot/ngx-errors';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { TaxListComponent } from './tax-list/tax-list.component';
import { TaxFormComponent } from './tax-form/tax-form.component';
import { SharedModule } from '../../shared/shared.module';
import { CategoryFormComponent } from './category-form/category-form.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderItemFormComponent } from './order-item-form/order-item-form.component';
import { OrderItemListComponent } from './order-item-list/order-item-list.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductListComponent } from './product-list/product-list.component';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
@NgModule({
  declarations: [   
    TaxListComponent,
    TaxFormComponent,
    CategoryFormComponent,
    CategoryListComponent,
    OrderFormComponent,
    OrderListComponent,
    OrderItemFormComponent,
    OrderItemListComponent,
    ProductFormComponent,
    ProductListComponent,
    CustomerFormComponent,
    CustomerListComponent,
    
  ],
  imports: [
    CommonModule,
    MasterdataRoutingModule,
    SharedModule,
    NgbPaginationModule,
    ReactiveFormsModule,
    NgxErrorsModule,
    
    FormsModule,
    NgbModalModule,
    SweetAlert2Module.forRoot()

  ]
})
export class MasterdataModule { }
