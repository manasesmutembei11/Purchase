import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FeatherModule } from 'angular-feather';
import { allIcons } from 'angular-feather/icons';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

import { TranslateModule } from '@ngx-translate/core';

import { VerticalComponent } from './vertical/vertical.component';
import { TopbarComponent } from './topbar/topbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { RightsidebarComponent } from './rightsidebar/rightsidebar.component';
import { HorizontalComponent } from './horizontal/horizontal.component';
import { HorizontaltopbarComponent } from './horizontaltopbar/horizontaltopbar.component';
import { SharedModule } from '../shared/shared.module';
import { LanguageService } from '../shared/services/language.service';
import { LayoutsComponent } from './layouts.component';
import { NgxSimplebarModule } from 'ngx-simplebar';
import { SimplebarAngularModule } from 'simplebar-angular';

@NgModule({
  declarations: [
    VerticalComponent,
    TopbarComponent,
    LayoutsComponent,
    SidebarComponent,
    FooterComponent,
    RightsidebarComponent,
    HorizontalComponent,
    HorizontaltopbarComponent
  ],
  imports: [
    CommonModule,
    TranslateModule,
    RouterModule,
    FeatherModule.pick(allIcons),
    NgbDropdownModule,
    SharedModule,
    NgxSimplebarModule,
    SimplebarAngularModule
    
  ],
  providers: [LanguageService],
  exports: [VerticalComponent]
})
export class LayoutsModule { }
