import { Component, OnInit } from '@angular/core';
import { title } from 'process';
import { IAdminAddress } from 'src/app/models/adminAddress.model';
import { IFilterAdminStreet } from 'src/app/models/filters/filterAdminStreet';
import { AdminService } from 'src/app/services/adminService';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  filter: IFilterAdminStreet = 
  { 
    title : "",
    description : ""
  };
  addresses: IAdminAddress[] = [];
  constructor(private service: AdminService) { }

  ngOnInit() {
      this.getAddress();
  }
  getAddress() : void{
    this.service.GetAddressStreets(this.filter).subscribe(res => this.addresses = res);
  }

}
