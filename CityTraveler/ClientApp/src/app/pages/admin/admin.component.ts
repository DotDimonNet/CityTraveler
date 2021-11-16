import { Component } from '@angular/core';
import { AdminService } from 'src/app/services/adminService';

@Component({
  selector: 'admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  providers:  [ AdminService ]
})
export class AdminComponent  {

  active: AdminTabs = "user";
  AddressSwitch(): string
  {
    console.log(this.active);
    return this.active = "address";
  }

  onSelect($event) {
    console.log($event);
    this.active = $event;
  }
}


export type AdminTabs = "user" | "address" | "review" | "trip"; 