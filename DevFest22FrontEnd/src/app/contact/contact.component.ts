import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GeneralService } from '../_services/general.service';
import { NgForm } from '@angular/forms';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';




@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  form1:NgForm;
  form2:NgForm;

  contactInfos$:Observable<any>;
  success:boolean;
  hasError:boolean = false;

  allfield = [{
    name_id: 1,
    name: 'Medical'
  },
  {
    name_id: 2,
    name: 'Health Care'
  },
  {
    name_id: 3,
    name: 'Pharmaceutical'
  },
  {
    name_id: 4,
    name: 'E-Commerce'
  },
  {
    name_id: 5,
    name: 'Business & International trade'
  },
  {
    name_id: 6,
    name: 'Mining'
  },
  {
    name_id: 7,
    name: 'Petroleum & Metallurgy'
  },
  {
    name_id: 8,
    name: 'Civil Engineering & Construction'
  },
  {
    name_id: 9,
    name: 'Marketing & Market Research'
  },
  {
    name_id: 10,
    name: 'Transport & Shipping'
  },
  {
    name_id: 11,
    name: 'Software & App localization'
  },
  {
    name_id: 12,
    name: 'Gaming'
  },
  {
    name_id: 13,
    name: 'Military'
  },
  {
    name_id: 14,
    name: 'Information Technology'
  },
  {
    name_id: 15,
    name: 'Automotive'
  },
  {
    name_id: 16,
    name: 'Finance & Banking'
  },
  {
    name_id: 17,
    name: 'Industrial Manufacturing'
  },
  {
    name_id: 18,
    name: 'Retail'
  },
  {
    name_id: 19,
    name: 'Travel & Hospitality'
  },
  {
    name_id: 20,
    name: 'Customer Care'
  },
  {
    name_id: 21,
    name: 'Other'
  },
]

allServices = [{
  id: 1,
  name: 'Translation'
},{
  id: 1,
  name: 'Transcription'
},{
  id: 1,
  name: 'Editing'
},{
  id: 1,
  name: 'Proofreading'
},{
  id: 1,
  name: 'MTPE'
},{
  id: 1,
  name: 'DTP'
},{
  id: 1,
  name: 'Transcreation'
},{
  id: 1,
  name: 'Software and website Testing'
},{
  id: 1,
  name: 'Multimedia Localization (Subtitling)'
},{
  id: 1,
  name: 'Voiceover'
},{
  id: 1,
  name: 'Other'
}
]

model: any={};
modelTwo: any={};


locations:any = {};
locationIsLoaded: boolean = false;
motherLanguages:any = {};
motherLanguagesIsLoaded: boolean = false;
nationalities:any = {};
nationalitiesIsLoaded: boolean = false;


  constructor(private gService:GeneralService, private mattap:MatTabsModule) {

   }

  ngOnInit(): void {
      this.contactInfos$ = this.gService.contactInfo();
      this.model.name_id = 2;
      this.modelTwo.id = 2;

      this.getLocations();
      this.getNationalities();
      this.getmthLanguages();
  }


  getLocations(): void {
    this.gService.location().subscribe(data => {
      if(data) {
        this.locations = data;
        this.locationIsLoaded = true;
        console.log(this.locations);
      }
    } );
  }

  getmthLanguages(): void {
    this.gService.motherLanguage().subscribe(data => {
      if(data) {
        this.motherLanguages = data;
        this.motherLanguagesIsLoaded = true;
        console.log(this.motherLanguages);
      }
    } );
  }

  getNationalities(): void {
    this.gService.nationality().subscribe(data => {
      if(data) {
        this.nationalities = data;
        this.nationalitiesIsLoaded = true;
        console.log(this.nationalities);
      }
    } );
  }






  sendMessage(form1:NgForm){
    console.log(form1.valid);
    console.log(form1.value);
    if(!form1.valid)
      return false;

      this.gService.newMessage(form1.value)
      .subscribe(_ =>{
        form1.reset();
        this.success = true;

        setTimeout(()=>{
          this.success = false;
        },2000)
      });
  }

  joinUs(form2:NgForm){
    console.log(form2.valid);
    console.log(form2.value);



    if(!form2.valid){
      return false;
    }

    form2[5]=

    this.gService.joinUs(form2.value)
    .subscribe(_ => {
      form2.reset();
      this.success = true;

      setTimeout(() => {
        this.success = false;
      }, 2000);
    })
  }

  linkUrl(url:string){
    var matches = url.match(/\/@([\d\.,-]+)z\//)[1];
    var splits = matches.split(',');
    var lat = splits[0];
    var long = splits[1];
    var zoom = splits[2];
    var link = 'http://maps.google.com/maps?q=' + lat + ',' + long + '&z=' + zoom + '&output=embed';
   return link;
  }

}

