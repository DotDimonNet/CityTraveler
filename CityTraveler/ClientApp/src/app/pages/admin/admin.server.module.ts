import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AdminModule } from './admin.module';
import { AdminComponent} from './admin.component'

@NgModule({
    imports: [AdminModule, ServerModule, ModuleMapLoaderModule],
    bootstrap: [AdminComponent]
})
export class AppServerModule { }
