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
import { UserManagementService } from './services/userManagementService';
import { UserManagementDataService } from './services/userManagementService.data';
import { DefaultTripPageComponent } from './pages/defaultTripPage/defaultTripPage.component';
import { DefaultTrip } from './models/defaultTrip.model';
import { TripService } from './services/tripService';
import { TripDataService } from './services/tripService.data';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserProfilePageComponent,
    DefaultTripPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user-profile', component: UserProfilePageComponent, pathMatch: 'full' },
      { path:'default-trip', component:DefaultTripPageComponent, pathMatch:'full'},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    UserManagementService,
    UserManagementDataService,
    TripService,
    TripDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
