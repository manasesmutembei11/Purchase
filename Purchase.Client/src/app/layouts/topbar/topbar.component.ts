import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

import { CookieService } from 'ngx-cookie-service';

import { TranslateService } from '@ngx-translate/core';


import { LAYOUT_MODE } from "../layouts.model";
import { filter, Subject, takeUntil } from 'rxjs';
import { User } from '../../core/models/user-models/user';
import { AuthService } from '../../core/services/auth-services/auth.service';
import { LanguageService } from '../../shared/services/language.service';
import { EventService } from '../../shared/services/event.service';


@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})

/**
 * Topbar Component
 */
export class TopbarComponent implements OnInit {
  user!: User;
  unsubscribe: Subject<Object| any> = new Subject<Object>();
  mode: string | undefined;
  element: any;
  flagvalue: any;
  cookieValue: any;
  countryName: any;
  valueset: any;

  constructor(
    private router: Router,
    private authService: AuthService,
    //private authFackservice: AuthfakeauthenticationService,
    public languageService: LanguageService,
    public _cookiesService: CookieService,
    public translate: TranslateService,
    private eventService: EventService
  ) { }

  /**
   * Language Listing
   */
  listLang = [
    { text: 'English', flag: 'assets/images/flags/us.jpg', lang: 'en' },
    // { text: 'Spanish', flag: 'assets/images/flags/spain.jpg', lang: 'es' },
    // { text: 'German', flag: 'assets/images/flags/germany.jpg', lang: 'de' },
    // { text: 'Italian', flag: 'assets/images/flags/italy.jpg', lang: 'it' },
    // { text: 'Russian', flag: 'assets/images/flags/russia.jpg', lang: 'ru' },
  ];

  @Output() settingsButtonClicked = new EventEmitter();
  @Output() mobileMenuButtonClicked = new EventEmitter();

  layoutMode!: string;

  ngOnInit(): void {
    this.layoutMode = LAYOUT_MODE;

    this.element = document.documentElement;
    // Cookies wise Language set
    this.cookieValue = this._cookiesService.get('lang');
    const val = this.listLang.filter(x => x.lang === this.cookieValue);
    this.countryName = val.map(element => element.text);
    if (val.length === 0) {
      if (this.flagvalue === undefined) { this.valueset = 'assets/images/flags/us.jpg'; }
    } else {
      this.flagvalue = val.map(element => element.flag);
    }
    this.authService.currentUser
      .pipe(
        takeUntil(this.unsubscribe),
        filter((user) => user != null)
      )
      .subscribe((user) => {
        this.user=user;
        //console.log('User info => ', user);
      });
  }
  ngOnDestroy() {
    this.unsubscribe.next(null);
    this.unsubscribe.complete();
   }
  /**
   * Language Value Set
   */
  setLanguage(text: string, lang: string, flag: string) {
    this.countryName = text;
    this.flagvalue = flag;
    this.cookieValue = lang;
    this.languageService.setLanguage(lang);
  }

  /**
   * Topbar Light-Dark Mode Change
   */
  changeMode(mode: string) {
    this.layoutMode = mode;
    this.mode = mode;
    this.eventService.broadcast('changeMode', mode);
  }

  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any) {
    event.preventDefault();
    this.mobileMenuButtonClicked.emit();
  }

  /**
   * Toggles the right sidebar
   */
  toggleRightSidebar() {
    this.settingsButtonClicked.emit();
  }

  /**
   * Logout the user
   */
  logout() {
   
      this.authService.logout();
    
    this.router.navigate(['/account/login']);
  }

}
