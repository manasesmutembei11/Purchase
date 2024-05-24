import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home/home-page/home-page.component';
import { CategoryListComponent } from './pages/masterdata/category-list/category-list.component';
import { CategoryFormComponent } from './pages/masterdata/category-form/category-form.component';
import { CustomerListComponent } from './pages/masterdata/customer-list/customer-list.component';
import { CustomerFormComponent } from './pages/masterdata/customer-form/customer-form.component';
import { OrderListComponent } from './pages/masterdata/order-list/order-list.component';
import { OrderFormComponent } from './pages/masterdata/order-form/order-form.component';
import { OrderItemListComponent } from './pages/masterdata/order-item-list/order-item-list.component';
import { OrderItemFormComponent } from './pages/masterdata/order-item-form/order-item-form.component';
import { ProductListComponent } from './pages/masterdata/product-list/product-list.component';
import { ProductFormComponent } from './pages/masterdata/product-form/product-form.component';
import { TaxListComponent } from './pages/masterdata/tax-list/tax-list.component';
import { TaxFormComponent } from './pages/masterdata/tax-form/tax-form.component';
import { ProductSelectionModalComponent } from './core/modals/product-selection-modal/product-selection-modal.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  {
    path: 'category',
    canActivate: [],
    children: [
      { path: '', component: CategoryListComponent, pathMatch: 'full',},
      { path: 'create', component: CategoryFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: CategoryFormComponent},
    ]
  },
  {
    path: 'customer',
    canActivate: [],
    children: [
      { path: '', component: CustomerListComponent, pathMatch: 'full',},
      { path: 'create', component: CustomerFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: CustomerFormComponent},
    ]
  },
  {
    path: 'order',
    canActivate: [],
    children: [
      { path: '', component: OrderListComponent, pathMatch: 'full',},
      { path: 'create', component: OrderFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: OrderFormComponent},
    ]
  },
  {
    path: 'orderItem',
    canActivate: [],
    children: [
      { path: '', component: OrderItemListComponent, pathMatch: 'full',},
      { path: 'create', component: OrderItemFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: OrderItemFormComponent},
    ]
  },
  {
    path: 'product',
    canActivate: [],
    children: [
      { path: '', component: ProductListComponent, pathMatch: 'full',},
      { path: 'create', component: ProductFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: ProductFormComponent},
    ]
  },
  {
    path: 'tax',
    canActivate: [],
    children: [
      { path: '', component: TaxListComponent, pathMatch: 'full',},
      { path: 'create', component: TaxFormComponent,pathMatch: 'full' },
      { path: 'edit/:id', component: TaxFormComponent},
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
