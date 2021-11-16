import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';  

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
import { EntertaimentfilterComponent} from './entertaimentfilter/entertaimentfilter.component'
import { SearchAddressComponent } from './addressfilter/search-address/search-address.component';
import { SearchReviewComponent } from './reviewsfilter/search-review/search-review.component';
import { SearchTripComponent } from './tripfilter/search-trip/search-trip.component';
import { SearchUserComponent } from './userfilter/search-user/search-user.component';
import { SearchEntertaimentComponent } from './entertaimentfilter/search-entertaiment/search-entertaiment.component';



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
    SearchAddressComponent,
    SearchReviewComponent,
    SearchTripComponent,
    SearchUserComponent,
    SearchEntertaimentComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    RouterModule.forChild([
      { path: 'admin', component: AdminComponent, pathMatch: 'full' },
      { path: 'admin/users', component: UserfilterComponent, pathMatch: 'full' },
      { path: 'admin/address', component: AddressfilterComponent, pathMatch: 'full' },
      { path: 'admin/trips', component: TripfilterComponent, pathMatch: 'full' },
      { path: 'admin/reviews', component: ReviewsfilterComponent, pathMatch: 'full' },
      { path: 'admin/entertaiments', component: EntertaimentfilterComponent, pathMatch: 'full' },
      { path: 'admin/statistic', component: StatisticComponent, pathMatch: 'full' },
      { path: 'admin/history', component: HistoryComponent, pathMatch: 'full' }
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
    SearchAddressComponent,
    SearchReviewComponent,
    SearchTripComponent,
    SearchUserComponent,
    SearchEntertaimentComponent
  ],
  bootstrap: [AdminComponent]
})
export class AdminModule { }
