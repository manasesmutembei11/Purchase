import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '../../shared/base/base-component';
import { AuthService } from '../../core/services/auth-services/auth.service';


@Component({
  selector: 'app-confirmmail',
  templateUrl: './confirmmail.component.html',
  styleUrls: ['./confirmmail.component.scss']
})

/**
 * Confirm-Mail Component
 */
export class ConfirmmailComponent extends BaseComponent implements OnInit {

  // set the currenr year
  year: number = new Date().getFullYear();
  // Carousel navigation arrow show
  showNavigationArrows: any;

  constructor(
    private route: ActivatedRoute,
    private authService:AuthService
  ) {
    super();
  }

  ngOnInit(): void {
    document.body.setAttribute('data-layout', 'vertical');
    this.confirmEmail();
  }
  
  private confirmEmail = () => {

    const token = this.route.snapshot.queryParams['token'];
    const email = this.route.snapshot.queryParams['email'];

    this.authService.confirmEmail( token, email)
    .subscribe({
      next: (_) => {},
      error: (errors) => {
        this.errors = errors;
      }
    })
  }

}
