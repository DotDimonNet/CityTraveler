import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AdminDataService } from "./adminDataService";
import { IAdminAddress } from "../models/adminAddress.model";


@Injectable()
export class AdminService {

    constructor(private dataService: AdminDataService) {}

    GetAddressStreets(filterAdminStreet) : Observable<IAdminAddress[]>{
        return this.dataService.GetAddressStreets(filterAdminStreet);
    }
}