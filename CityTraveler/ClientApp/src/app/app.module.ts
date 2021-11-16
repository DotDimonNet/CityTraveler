import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ComComponent } from './com/com.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserProfilePageComponent } from './pages/userProfile/components/userProfilePage/userProfilePage.component';
import { EntertainmentModule } from './pages/Entertainment/entertainment.module';
import { SocialMediaPageComponent } from './pages/socialMedia/socialMediaPage.component';
import { UserManagementService } from './services/userManagementService';
import { UserManagementDataService } from './services/userManagementService.data';
import { UserInfoComponent } from './pages/userProfile/components/userInfo/userInfo.component';
import { StatisticDataService } from './services/StatisticService.data';
import { StatisticService } from './services/StatisticService';
import { AdminComponent } from './pages/admin/admin.component';
import { AdminService } from './services/adminService';
import { AdminDataService } from './services/adminService.data';
import { EntertainmentDataService } from './services/entertainmentService.data';
import { EntertainmentService } from './services/entertainmentService';
import { DefaultTripPageComponent } from './pages/defaultTrip/defaultTripPage.component';
import { DefaultTrip } from './models/defaultTrip.model';
import { TripService } from './services/tripService';
import { TripDataService } from './services/tripService.data';
import { SocialMediaDataService } from './services/socialMediaService.data';
import { SocialMediaService } from './services/socialMediaService';
import { AddReviewTripPageComponent } from './pages/socialMedia/addReviewTripPage.component';
import { DeleteReviewPageComponent } from './pages/socialMedia/deleteReviewPage.component';
import { DeleteCommentPageComponent } from './pages/socialMedia/deleteCommentPage.component';
import { InfoDataService } from './services/InfoService.data';
import { InfoService } from './services/InfoService';
import { SearchDataService } from './services/searchService.data';
import { SearchService } from './services/searchService';
import { SearchServiceComponent } from './pages/searchService/searchService.component';
import { SearchUsersComponent } from './pages/searchService/searchUsersPage.component';
import { SearchTripsComponent } from './pages/searchService/searchTrips.component';
import { AdminModule } from './pages/admin/admin.module';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ComComponent,
    CounterComponent,
    FetchDataComponent,
    UserProfilePageComponent,
    UserInfoComponent,
    DefaultTripPageComponent,
    SocialMediaPageComponent,
    AddReviewTripPageComponent,
    DeleteReviewPageComponent,
    DeleteCommentPageComponent,
    SearchServiceComponent,
    SearchTripsComponent,
    SearchUsersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    EntertainmentModule,
    AdminModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user-profile', component: UserProfilePageComponent, pathMatch: 'full' },
      { path: 'default-trip', component:DefaultTripPageComponent, pathMatch:'full'},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'com', component: ComComponent },
      { path: 'admin', component: AdminComponent, pathMatch:'full'},
      { path: 'review-model', component: SocialMediaPageComponent, pathMatch: 'full' },
      { path: 'add-review-trip', component: AddReviewTripPageComponent, pathMatch: 'full' },
      { path: 'delete-review', component: DeleteReviewPageComponent, pathMatch: 'full' },
      { path: 'delete-comment', component: DeleteCommentPageComponent, pathMatch: 'full' },
      { path: 'search-entertainments', component: SearchServiceComponent, pathMatch: 'full' },
      { path: 'search-users', component: SearchUsersComponent, pathMatch: 'full' },
      { path: 'search-trips', component: SearchTripsComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    UserManagementService,
    UserManagementDataService,
    StatisticDataService,
    StatisticService,
    AdminDataService,
    AdminService,
    TripService,
    TripDataService,
    SocialMediaDataService,
    SocialMediaService,
    SearchDataService,
    SearchService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
