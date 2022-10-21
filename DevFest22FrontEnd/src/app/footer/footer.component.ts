import { Observable } from 'rxjs';
import { GeneralService } from './../_services/general.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  infos$:Observable<any>;
  constructor(private gService:GeneralService) { }

  ngOnInit(): void {
    this.infos$ = this.gService.contactInfo();
  }

}
