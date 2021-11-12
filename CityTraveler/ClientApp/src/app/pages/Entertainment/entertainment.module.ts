import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { EntertainmentShowComponent } from './EntertainmentShow/entertainment.component';
import { EntertainmentPreviewComponent } from './entertainmentPreview/entertainment.preview.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EntertainmentDataService } from 'src/app/services/entertainmentService.data';
import { EntertainmentService } from 'src/app/services/entertainmentService';
import { FindArea } from './FindArea/findArea.component';


@NgModule({
  declarations: [
    EntertainmentShowComponent,
    EntertainmentPreviewComponent,
    FindArea,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'entertainment', component: FindArea, pathMatch: 'full' },
    ])
  ],
  providers: [
    EntertainmentDataService,
    EntertainmentService,
  ],
  bootstrap: [
    EntertainmentPreviewComponent,
    EntertainmentShowComponent,
    FindArea,
  ]
})
export class EntertainmentModule { }
