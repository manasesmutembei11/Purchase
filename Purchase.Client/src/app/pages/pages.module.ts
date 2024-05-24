import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomePageComponent } from './home/home-page/home-page.component';




@NgModule({
  declarations: [    
    HomePageComponent, 
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    SharedModule,
   
  ]
})
export class PagesModule { }
