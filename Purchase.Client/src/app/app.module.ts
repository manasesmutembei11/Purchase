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
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
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
    ProductFormComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, RouterModule, NgbModule, CommonModule, FormsModule, ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
