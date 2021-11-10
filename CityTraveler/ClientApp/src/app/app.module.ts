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
import { UserManagementService } from './services/userManagementService';
import { UserManagementDataService } from './services/userManagementService.data';
import { StatisticDataService } from './services/StatisticService.data';
import { StatisticService } from './services/StatisticService';
import { AdminComponent } from './pages/admin/admin.component';
import { AdminDataService } from './services/adminService.data';
import { AdminService } from './services/admin.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ComComponent,
    CounterComponent,
    FetchDataComponent,
    UserProfilePageComponent,
    AdminComponent
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
      { path: 'com', component: ComComponent },
      { path: 'admin', component: AdminComponent}
    ])
  ],
  providers: [
    UserManagementService,
    UserManagementDataService,
    StatisticDataService,
    StatisticService,
    AdminDataService,
    AdminService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
