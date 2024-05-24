import { Component, OnInit } from '@angular/core';
import { RouteConfigLoadEnd, RouteConfigLoadStart, Router } from '@angular/router';
import { first } from 'rxjs';

import { MenuItem } from '../sidebar/menu.model';
import { MenuService } from '../../shared/services/menu.service';

@Component({
  selector: 'app-vertical',
  templateUrl: './vertical.component.html',
  styleUrls: ['./vertical.component.scss']
})

/**
 * Vertical Component
 */
export class VerticalComponent implements OnInit {
  isLoading: boolean=false;
  isCondensed = false;
  menuItems: MenuItem[]=[];
  constructor(private menuService:MenuService,private router: Router) { 
    router.events.forEach((event) => {
      if (event instanceof RouteConfigLoadStart) {
        this.isLoading = true;
        console.log("Start Loading ...");        
      } else if (event instanceof RouteConfigLoadEnd) {
       this.isLoading = false;  
       console.log("End Loading ...");             
      }
    });
  }
  ngOnInit(): void {
    document.body.setAttribute('data-layout', 'vertical');
    this.menuService.getMenu()
    .pipe(first())
    .subscribe({
      next:(_)=>{
        this.menuItems=_
      }
    })
  }

  /**
   * On mobile toggle button clicked
   */
  onToggleMobileMenu() {
    document.body.classList.toggle('sidebar-enable');
    const currentSIdebarSize = document.body.getAttribute("data-sidebar-size");
    if (window.screen.width >= 992) {
      if (currentSIdebarSize == null) {
        (document.body.getAttribute('data-sidebar-size') == null || document.body.getAttribute('data-sidebar-size') == "lg") ? document.body.setAttribute('data-sidebar-size', 'sm') : document.body.setAttribute('data-sidebar-size', 'lg')
      } else if (currentSIdebarSize == "md") {
        (document.body.getAttribute('data-sidebar-size') == "md") ? document.body.setAttribute('data-sidebar-size', 'sm') : document.body.setAttribute('data-sidebar-size', 'md')
      } else {
        (document.body.getAttribute('data-sidebar-size') == "sm") ? document.body.setAttribute('data-sidebar-size', 'lg') : document.body.setAttribute('data-sidebar-size', 'sm')
      }
    }
    this.isCondensed = !this.isCondensed;
  }

  /**
   * on settings button clicked from topbar
   */
  onSettingsButtonClicked() {
    document.body.classList.toggle('right-bar-enabled');
  }

}
