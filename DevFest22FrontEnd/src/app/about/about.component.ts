import { GeneralService } from './../_services/general.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  abouts$:Observable<any>
  constructor(private gService:GeneralService) { }

  ngOnInit(): void {
    this.abouts$ = this.gService.about();
  }

}
