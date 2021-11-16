import { Component, OnInit, ViewChild } from '@angular/core';
import { IAdminAddress } from 'src/app/models/adminAddress.model';
import { IFilterAdminStreet } from 'src/app/models/filters/filterAdminStreet';
import { AdminService } from 'src/app/services/adminService';
import { NavAdminComponent } from './nav-admin/nav-admin.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  providers:  [ AdminService ]
})
export class AdminComponent  {

  active: string = "user";
  AddressSwitch(): string
  {
    console.log(this.active);
    return this.active = "address";
  }
  CheckSwitch(): string
  {
    return this.active 
  }
}
