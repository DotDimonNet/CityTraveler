import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AdminDataService } from "./AdminService.data";
import { IAdminAddress } from "../models/adminAddress.model";
import { map } from "rxjs/operators";


@Injectable({
    providedIn: 'root'
    })
export class AdminService {

    constructor(private dataService: AdminDataService) {}

    GetAddressStreets(filterAdminStreet) : Observable<IAdminAddress[]>{
        return this.dataService.GetAddressStreets(filterAdminStreet);
    }
}