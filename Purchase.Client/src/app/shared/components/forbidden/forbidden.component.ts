import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.scss']
})
export class ForbiddenComponent implements OnInit {
  type: number=403;
  title: any="Forbidden";
  desc: any= "You dont have permission to access the resource. Please contact your administrator";
  constructor() { }

  ngOnInit(): void {
  }

}
