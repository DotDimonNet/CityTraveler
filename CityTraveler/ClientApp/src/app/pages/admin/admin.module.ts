import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { AdminComponent } from './admin.component';
import { AdminDataService } from 'src/app/services/AdminService.data';
import { AdminService } from 'src/app/services/adminService';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AdminComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    AdminDataService,
    AdminService
   
  ],
  bootstrap: [AdminComponent]
})
export class AdminModule { }
