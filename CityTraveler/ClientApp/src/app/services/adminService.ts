import { Injectable } from '@angular/core';
import { AdminDataService } from './AdminService.data';

@Injectable()
export class AdminService {

  constructor(private dataService: AdminDataService) { }
  GetAddressStreet() {
    return this.dataService.GetAddressStreet();
}
}
