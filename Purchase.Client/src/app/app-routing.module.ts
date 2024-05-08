import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home/home-page/home-page.component';
import { CategoryListComponent } from './components/masterdata/category-list/category-list.component';
import { CategoryFormComponent } from './components/masterdata/category-form/category-form.component';
import { CustomerListComponent } from './components/masterdata/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/masterdata/customer-form/customer-form.component';

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
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
