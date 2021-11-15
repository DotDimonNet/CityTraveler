import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { AdminComponent } from './admin.component';
import { AdminDataService } from 'src/app/services/AdminService.data';
import { AdminService } from 'src/app/services/adminService';
import { NavAdminComponent } from './nav-admin/nav-admin.component';
import { AddressfilterComponent } from './addressfilter/addressfilter.component';
import { TripfilterComponent } from './tripfilter/tripfilter.component';
import { StatisticComponent } from '../statistic/statistic.component';
import { HistoryComponent } from '../history/history.component';
import { UserfilterComponent } from './userfilter/userfilter.component';
import { ReviewsfilterComponent } from './reviewsfilter/reviewsfilter.component';
import {EntertaimentfilterComponent} from './entertaimentfilter/entertaimentfilter.component'
import { SearchAddressComponent } from './search-address/search-address.component';



@NgModule({
  declarations: [
    NavAdminComponent,
    AddressfilterComponent,
    TripfilterComponent,
    StatisticComponent,
    HistoryComponent,
    UserfilterComponent,
    ReviewsfilterComponent,
    EntertaimentfilterComponent,
    SearchAddressComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: AdminComponent, pathMatch: 'full' },
      { path: 'users', component: UserfilterComponent, pathMatch: 'full' },
      { path: 'address', component: AddressfilterComponent, pathMatch: 'full' },
      { path: 'trips', component: TripfilterComponent, pathMatch: 'full' },
      { path: 'reviews', component: ReviewsfilterComponent, pathMatch: 'full' },
      { path: 'entertaiments', component: EntertaimentfilterComponent, pathMatch: 'full' },
      { path: 'statistic', component: StatisticComponent, pathMatch: 'full' },
      { path: 'history', component: HistoryComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    AdminDataService,
    AdminService
   
  ],
  exports:[
    NavAdminComponent,
    AddressfilterComponent,
    TripfilterComponent,
    StatisticComponent,
    HistoryComponent,
    UserfilterComponent,
    ReviewsfilterComponent,
    EntertaimentfilterComponent,
    SearchAddressComponent
  ],
  bootstrap: [AdminComponent]
})
export class AdminModule { }
