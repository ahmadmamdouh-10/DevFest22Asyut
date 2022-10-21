import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn :'root'
})
export class GeneralService{

    constructor(private http:HttpClient) {
    }

    about(){
        return this.http.get<any>(`${environment.BaseUrl}/api/aboutUs`);
    }

    articles(){
        return this.http.get<any>(`${environment.BaseUrl}/api/blog`);
    }

    article(id:number){
        return this.http.get<any>(`${environment.BaseUrl}/api/blog/${id}`);
    }

    contactInfo(){
        return this.http.get<any>(`${environment.BaseUrl}/api/contactInfo`);
    }

    joinUs(message:any){
      return this.http.post<any>(`${environment.BaseUrl}/api/contactInfo/JoinUs`,message);
  }
}
