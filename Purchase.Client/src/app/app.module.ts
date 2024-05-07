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

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    CategoryListComponent,
    PagetitleComponent,
    CategoryFormComponent,
    ErrorDisplayComponent,
    ValidityStyleDirective
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, RouterModule, NgbModule, CommonModule, FormsModule, ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
