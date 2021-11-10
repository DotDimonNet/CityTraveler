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
import { UserProfilePageComponent } from './pages/userProfile/userProfilePage.component';
import { EntertainmentComponent } from './pages/entertainment/entertainment.component';
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
import { DefaultTripPageComponent } from './pages/defaultTripPage/defaultTripPage.component';
import { DefaultTrip } from './models/defaultTrip.model';
import { TripService } from './services/tripService';
import { TripDataService } from './services/tripService.data';
import { SocialMediaDataService } from './services/socialMediaService.data';
import { SocialMediaService } from './services/socialMediaService';
import { AddReviewTripPageComponent } from './pages/socialMedia/addReviewTripPage.component';
import { DeleteReviewPageComponent } from './pages/socialMedia/deleteReviewPage.component';
import { DeleteCommentPageComponent } from './pages/socialMedia/deleteCommentPage.component';


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
    AdminComponent,
    EntertainmentComponent,
    DefaultTripPageComponent,
    SocialMediaPageComponent,
    AddReviewTripPageComponent,
    DeleteReviewPageComponent,
    DeleteCommentPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user-profile', component: UserProfilePageComponent, pathMatch: 'full' },
      { path: 'entertainment', component: EntertainmentComponent, pathMatch: 'full' },
      { path:'default-trip', component:DefaultTripPageComponent, pathMatch:'full'},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'com', component: ComComponent },
      { path: 'admin', component: AdminComponent}
      { path: 'review-model', component: SocialMediaPageComponent, pathMatch: 'full' },
      { path: 'add-review-trip', component: AddReviewTripPageComponent, pathMatch: 'full' },
      { path: 'delete-review', component: DeleteReviewPageComponent, pathMatch: 'full' },
      { path: 'delete-comment', component: DeleteCommentPageComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    UserManagementService,
    UserManagementDataService,
    StatisticDataService,
    StatisticService,
    AdminDataService,
    AdminService,
    EntertainmentDataService,
    EntertainmentService
    TripService,
    TripDataService
    SocialMediaDataService,
    SocialMediaService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
