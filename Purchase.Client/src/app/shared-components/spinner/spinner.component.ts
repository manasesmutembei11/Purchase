import { Component, OnInit } from '@angular/core';
import { LoadingIndicatorService } from '../../services/shared-services/loading-indicator-service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {
  loading: boolean = false;
  constructor(private loadingIndicatorService: LoadingIndicatorService) {

  }

  ngOnInit(): void {
    this.loadingIndicatorService
    .onLoadingChanged
    .subscribe(isLoading =>{
      setTimeout(()=> {
        this.loading = isLoading
      },0)
    });
  }

}
