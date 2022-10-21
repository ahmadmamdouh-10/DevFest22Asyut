import { ArticleDetailsComponent } from './blog/article-details/article-details.component';
import { BlogComponent } from './blog/blog.component';
import { ContactComponent } from './contact/contact.component';
import { LanguageComponent } from './language/language.component';
import { ToolComponent } from './tool/tool.component';
import { QualityAndStandardComponent } from './quality-and-standard/quality-and-standard.component';
import { IndustryComponent } from './industry/industry.component';
import { AboutComponent } from './about/about.component';
import { ServiceComponent } from './service/service.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {component : HomeComponent , path :''},
  {component : ServiceComponent , path :'services'},
  {component : AboutComponent , path :'about'},
  {component : IndustryComponent , path :'industries'},
  {component : ToolComponent , path :'tools'},
  {component : LanguageComponent , path :'language'},
  {component : ContactComponent , path :'contact'},
  {component : BlogComponent , path :'blog'},
  {component : ArticleDetailsComponent , path :'article/:id'},
  {component : QualityAndStandardComponent , path :'qualityAndStandard'},
  {path :'**' , redirectTo :'/' ,pathMatch : 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
