import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AdminTabs } from '../admin.component';

@Component({
  selector: 'nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.css']
})
export class NavAdminComponent implements OnInit {

  @Output() onSelect: EventEmitter<AdminTabs> = new EventEmitter<AdminTabs>();
  public tabs: ITabInfo[] = [];
  constructor() {
  }

  goTo(tabName: AdminTabs) {
    this.onSelect.emit(tabName);
  }

  ngOnInit() {
    this.tabs = [
      { tabName: "user", displayName: "Users" },
      { tabName: "trip", displayName: "Trips" },
      { tabName: "review", displayName: "Reviews" },
      { tabName: "address", displayName: "Addresses" }
    ];
  }
}

interface ITabInfo {
  tabName: string;
  displayName: string;
}
