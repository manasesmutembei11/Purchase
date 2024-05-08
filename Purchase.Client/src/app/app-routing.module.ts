import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home/home-page/home-page.component';
import { CategoryListComponent } from './components/masterdata/category-list/category-list.component';
import { CategoryFormComponent } from './components/masterdata/category-form/category-form.component';
import { CustomerListComponent } from './components/masterdata/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/masterdata/customer-form/customer-form.component';
import { OrderListComponent } from './components/masterdata/order-list/order-list.component';
import { OrderFormComponent } from './components/masterdata/order-form/order-form.component';
import { OrderItemListComponent } from './components/masterdata/order-item-list/order-item-list.component';
import { OrderItemFormComponent } from './components/masterdata/order-item-form/order-item-form.component';
import { ProductListComponent } from './components/masterdata/product-list/product-list.component';
import { ProductFormComponent } from './components/masterdata/product-form/product-form.component';

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
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
