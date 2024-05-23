import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppVersionComponent } from './app-version/app-version.component';
import { BannerComponent } from './banner/banner.component';
import { ErrorDisplayComponent } from '../components/shared/error-display/error-display.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { LogoComponent } from './logo/logo.component';
import { PagetitleComponent } from '../components/shared/pagetitle/pagetitle.component';



@NgModule({
  declarations: [
    BannerComponent,
    AppVersionComponent,
    ErrorDisplayComponent,
    ErrorPageComponent,
    LogoComponent,
    PagetitleComponent

  
  ],
  imports: [CommonModule ],
  exports: [
    BannerComponent



  ],
})
export class SharedModule {}
