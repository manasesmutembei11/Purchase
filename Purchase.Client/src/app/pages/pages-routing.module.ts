import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForbiddenComponent } from '../shared/components/forbidden/forbidden.component';
import { HomePageComponent } from '../home-page/home-page.component';
import { AppComponent } from '../app.component';



const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'Forbidden', component: ForbiddenComponent },   
  { path: 'masterdata', loadChildren: () => import('./masterdata/masterdata.module').then(m => m.MasterdataModule) },  

  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
