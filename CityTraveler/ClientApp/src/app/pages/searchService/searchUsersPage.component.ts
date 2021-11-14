import { Component } from "@angular/core";
import { IFiltertUsers } from "src/app/models/filters/filterUsers";
import { IUserProfile } from "src/app/models/user.model";
import { SearchService } from "src/app/services/searchService";

@Component({
    selector: 'search-users',
    templateUrl: './searchUsersPage.component.html',
    styleUrls: ['./searchService.component.css']
  })

export class SearchUsersComponent {
    public users: IUserProfile [] = [];
    public props: IFiltertUsers = {
      name: "",
      entertainmentName: "",
      gender: ""
    }; 
    constructor(private service: SearchService) 
    {
    }
    submitUsers()
    {
      this.service.getUsers(this.props).subscribe((res: IUserProfile[]) => {
        this.users = res;
       });
    }
}