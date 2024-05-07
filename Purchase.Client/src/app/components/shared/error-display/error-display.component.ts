import { Component, Input, OnInit } from '@angular/core';
import { Error } from '../../../models/shared-models/error';



@Component({
  selector: 'app-error-display',
  templateUrl: './error-display.component.html',
  styleUrls: ['./error-display.component.scss']
})
export class ErrorDisplayComponent implements OnInit {

  @Input() errors!: Error[] ;
  constructor() {

  }

  ngOnInit(): void {

  }

}
