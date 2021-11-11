import { Component, OnInit } from '@angular/core';
import { IAdminAddress } from 'src/app/models/adminAddress.model';
import { IFilterAdminStreet } from 'src/app/models/filters/filterAdminStreet';
import { AdminService } from 'src/app/services/adminService';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  providers:  [ AdminService ]
})
export class AdminComponent implements OnInit {

  filter: IFilterAdminStreet = 
  { 
    title : "",
    description : ""
  };
  addresses: IAdminAddress[] = [];
  pesp: any;
  constructor(private service: AdminService) { }

  ngOnInit() {
      this.getAddress();
  }
  getAddress() : void{
    console.log(this.addresses);
    this.service.GetAddressStreets(this.filter).subscribe(res => this.addresses = res);
    console.log(this.addresses);
  }

}
