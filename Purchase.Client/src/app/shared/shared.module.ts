import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidityStyleDirective } from './directives/validity-style.directive';
import { ErrorDisplayComponent } from './components/error-display/error-display.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PagetitleComponent } from './components/pagetitle/pagetitle.component';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { LogoComponent } from './components/logo/logo.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { AppVersionComponent } from './components/app-version/app-version.component';
import { BannerComponent } from './components/banner/banner.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';


@NgModule({
  declarations: [
    
    ValidityStyleDirective,
    ErrorDisplayComponent, 
    PagetitleComponent,
    LogoComponent,
    SpinnerComponent,
    AppVersionComponent,
    BannerComponent,
    ForbiddenComponent,
    
  ],
  imports: [CommonModule, ReactiveFormsModule, FormsModule,NgbCarouselModule ],
  exports: [
    
    ValidityStyleDirective,
    ErrorDisplayComponent,
    PagetitleComponent,
  ],
})
export class SharedModule {}
