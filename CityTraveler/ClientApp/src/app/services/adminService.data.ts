import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { title } from "process";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IAdminAddress } from "../models/adminAddress.model";
import { IFilterAdminStreet } from "../models/filters/filterAdminStreet";

@Injectable()
export class AdminDataService {

    constructor(private client: HttpClient) {}

    GetAddressStreets(filterAdminStreet) : Observable<IAdminAddress[]> {
        return this.client.get(`/api/admin/get-streets?title=${filterAdminStreet.title}&description=${filterAdminStreet.description}`)
        .pipe(first(), map((res: IAdminAddress[]) => {
            return res as IAdminAddress[];
        }));
    }
}