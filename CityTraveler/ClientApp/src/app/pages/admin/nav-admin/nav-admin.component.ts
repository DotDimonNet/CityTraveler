import { Component, OnInit, Optional } from '@angular/core';
import { AdminComponent } from '../admin.component';

@Component({
  selector: 'app-nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.css']
})
export class NavAdminComponent implements OnInit {

  constructor(@Optional() private parent: AdminComponent) {
  }
  AddressSwith(){
    this.parent.AddressSwitch();
  }
  ngOnInit() {
  }

}
