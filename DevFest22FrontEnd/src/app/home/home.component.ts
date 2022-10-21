import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { GeneralService } from './../_services/general.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  services$ : Observable<any>;
  industries$ : Observable<any>;
  aboutUs$ : Observable<any>;
  qualities :Observable<any>;
  success:boolean;
  modalRef: BsModalRef;

  options:OwlOptions = {
    loop:true,
    items : 3,
    dots : true,
    navSpeed : 1000,
    autoplay : true,
    nav : false,
    responsive: {
      0:{
          items:1
      },
      576:{
          items:1
      },
      768:{
          items:2
      },
      992:{
          items:3
      }
    }
  }

  constructor(private gService:GeneralService,
              private modalService: BsModalService) {}

  ngOnInit(): void {
    this.services$ = this.gService.services();
    this.industries$ = this.gService.industries();
    this.aboutUs$ = this.gService.about();
    this.gService.qualityAndStandard().subscribe((res=>{
      this.qualities = res;
    }));
    console.log(this.qualities);
  }


  sendMessage(form:NgForm){
    if(!form.valid)
      return false;

      this.gService.newMessage(form.value)
      .subscribe(_ =>{
        form.reset();
        this.success = true;

        setTimeout(()=>{
          this.success = false;
        },2000)
      });
  }
  openModal(contactTemplate: TemplateRef<any>){
    this.modalRef = this.modalService.show(contactTemplate);
  }
}
