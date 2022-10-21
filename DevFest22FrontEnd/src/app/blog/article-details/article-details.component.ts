import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GeneralService } from 'src/app/_services/general.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent implements OnInit {

  id = +this.route.snapshot.paramMap.get('id');
  article$:Observable<any>;
  constructor(private gService:GeneralService,private route:ActivatedRoute) { }

  ngOnInit(): void {
      this.article$ = this.gService.article(this.id);
  }

}
