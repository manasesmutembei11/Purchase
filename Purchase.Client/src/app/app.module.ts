import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ErrorDisplayComponent } from './shared/components/error-display/error-display.component';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home/home-page/home-page.component';
import { NgbModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryListComponent } from './pages/masterdata/category-list/category-list.component';
import { PagetitleComponent } from './shared/components/pagetitle/pagetitle.component';
import { CategoryFormComponent } from './pages/masterdata/category-form/category-form.component';
import { ValidityStyleDirective } from './shared/directives/validity-style.directive';
import { CustomerListComponent } from './pages/masterdata/customer-list/customer-list.component';
import { CustomerFormComponent } from './pages/masterdata/customer-form/customer-form.component';
import { OrderListComponent } from './pages/masterdata/order-list/order-list.component';
import { OrderFormComponent } from './pages/masterdata/order-form/order-form.component';
import { OrderItemFormComponent } from './pages/masterdata/order-item-form/order-item-form.component';
import { OrderItemListComponent } from './pages/masterdata/order-item-list/order-item-list.component';
import { ProductListComponent } from './pages/masterdata/product-list/product-list.component';
import { ProductFormComponent } from './pages/masterdata/product-form/product-form.component';
import { TaxFormComponent } from './pages/masterdata/tax-form/tax-form.component';
import { TaxListComponent } from './pages/masterdata/tax-list/tax-list.component';
import { ProductSelectionModalComponent } from './core/modals/product-selection-modal/product-selection-modal.component';
import { CategorySelectionModalComponent } from './core/modals/category-selection-modal/category-selection-modal.component';
import { OrderItemSelectionModalComponent } from './core/modals/order-item-selection-modal/order-item-selection-modal.component';
import { CustomerSelectionModalComponent } from './core/modals/customer-selection-modal/customer-selection-modal.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { TaxModalComponent } from './core/modals/tax-modal/tax-modal.component';

import { LayoutsComponent } from './layouts/layouts.component';

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
    TaxModalComponent,
    LayoutsComponent
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
