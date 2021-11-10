import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserProfilePageComponent } from './pages/userProfile/userProfilePage.component';
import { SocialMediaPageComponent } from './pages/socialMedia/socialMediaPage.component';
import { UserManagementService } from './services/userManagementService';
import { UserManagementDataService } from './services/userManagementService.data';
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
    CounterComponent,
    FetchDataComponent,
    UserProfilePageComponent,
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
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      {path: 'review-model', component: SocialMediaPageComponent, pathMatch: 'full' },
      {path: 'add-review-trip', component: AddReviewTripPageComponent, pathMatch: 'full' },
      {path: 'delete-review', component: DeleteReviewPageComponent, pathMatch: 'full' },
      {path: 'delete-comment', component: DeleteCommentPageComponent, pathMatch: 'full' }
    ])
  ],
  providers: [
    UserManagementService,
    UserManagementDataService,
    SocialMediaDataService,
    SocialMediaService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
