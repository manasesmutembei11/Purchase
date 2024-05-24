import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaxListComponent } from './tax-list/tax-list.component';
import { TaxFormComponent } from './tax-form/tax-form.component';
import { AuthGuard } from '../../shared/guards/auth.guard';
import { HomePageComponent } from '../home/home-page/home-page.component';
import { CategoryFormComponent } from './category-form/category-form.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CustomerFormComponent } from './customer-form/customer-form.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderItemListComponent } from './order-item-list/order-item-list.component';
import { OrderItemFormComponent } from './order-item-form/order-item-form.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductListComponent } from './product-list/product-list.component';




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
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterdataRoutingModule { }
