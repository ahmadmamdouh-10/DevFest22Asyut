import { environment } from './../../environments/environment';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { GeneralService } from '../_services/general.service';
import { Observable } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

  modalRef: BsModalRef;
  articles$:Observable<any>;
  modalContent : string;
  article$:Observable<any>;
  constructor(private gService:GeneralService,
              private modalService: BsModalService) { }

  ngOnInit(): void {
      this.articles$ = this.gService.articles();
  }

  
  openModal(template: TemplateRef<any>,articleId) {
    this.modalRef = this.modalService.show(template);
    this.modalContent = `${location.href.substring(0,location.href.indexOf('#'))}/#/article/${articleId}`;
    
    this.article$ =  this.gService.article(articleId);
  }

  closeModal() {
    this.modalRef.hide();
  }

  copyToClipboard(text) {
    document.addEventListener('copy', (e: ClipboardEvent) => {
      e.clipboardData.setData('text/plain', (text));
      e.preventDefault();
      document.removeEventListener('copy', null);
    });
    document.execCommand('copy');
  }
}
