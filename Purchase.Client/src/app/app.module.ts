import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ErrorDisplayComponent } from './components/shared/error-display/error-display.component';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './components/home/home-page/home-page.component';
import { NgbModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryListComponent } from './components/masterdata/category-list/category-list.component';
import { PagetitleComponent } from './components/shared/pagetitle/pagetitle.component';
import { CategoryFormComponent } from './components/masterdata/category-form/category-form.component';
import { ValidityStyleDirective } from './services/directives/validity-style.directive';
import { CustomerListComponent } from './components/masterdata/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/masterdata/customer-form/customer-form.component';
import { OrderListComponent } from './components/masterdata/order-list/order-list.component';
import { OrderFormComponent } from './components/masterdata/order-form/order-form.component';
import { OrderItemFormComponent } from './components/masterdata/order-item-form/order-item-form.component';
import { OrderItemListComponent } from './components/masterdata/order-item-list/order-item-list.component';
import { ProductListComponent } from './components/masterdata/product-list/product-list.component';
import { ProductFormComponent } from './components/masterdata/product-form/product-form.component';
import { TaxFormComponent } from './components/masterdata/tax-form/tax-form.component';
import { TaxListComponent } from './components/masterdata/tax-list/tax-list.component';
import { ProductSelectionModalComponent } from './components/modals/product-selection-modal/product-selection-modal.component';
import { CategorySelectionModalComponent } from './components/modals/category-selection-modal/category-selection-modal.component';
import { OrderItemSelectionModalComponent } from './components/modals/order-item-selection-modal/order-item-selection-modal.component';
import { CustomerSelectionModalComponent } from './components/modals/customer-selection-modal/customer-selection-modal.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { OrderModalComponent } from './components/modals/order-modal/order-modal.component';
import { TaxModalComponent } from './components/modals/tax-modal/tax-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    CategoryListComponent,
    PagetitleComponent,
    CategoryFormComponent,
    ErrorDisplayComponent,
    ValidityStyleDirective,
    CustomerListComponent,
    CustomerFormComponent,
    OrderListComponent,
    OrderFormComponent,
    OrderItemFormComponent,
    OrderItemListComponent,
    ProductListComponent,
    ProductFormComponent,
    TaxFormComponent,
    TaxListComponent,
    ProductSelectionModalComponent,
    CategorySelectionModalComponent,
    OrderItemSelectionModalComponent,
    CustomerSelectionModalComponent,
    OrderModalComponent,
    TaxModalComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, RouterModule, NgbModule, CommonModule, FormsModule, ReactiveFormsModule, MatCardModule,
    MatIconModule
  ],
  providers: [NgbModal, provideAnimationsAsync()],
  bootstrap: [AppComponent]
})
export class AppModule { }
