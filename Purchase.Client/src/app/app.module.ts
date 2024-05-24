import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { LayoutsComponent } from './layouts/layouts.component';

@NgModule({ declarations: [
        AppComponent,
        LayoutsComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule, RouterModule, NgbModule, CommonModule, FormsModule, ReactiveFormsModule, MatCardModule,
        MatIconModule], providers: [NgbModal, provideAnimationsAsync(), provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
