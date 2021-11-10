import { TemplateParser } from '@angular/compiler';
import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { Observable } from 'rxjs';
import { IEntertainment } from 'src/app/models/entertainment.model';
import { IUserProfile } from 'src/app/models/user.model';
import { EntertainmentService } from 'src/app/services/entertainmentService';
import { isIdentifierStart } from 'typescript';

@Component({
  selector: 'entertainment',
  templateUrl: './entertainment.component.html',
  styleUrls: ['./entertainment.component.css']
})
export class EntertainmentComponent implements OnInit{
  public entertainmentInfo: IEntertainment = {
      id: "",
      title: "",
      type: "",
      address: {
        street: {
          title: ""
        },
        houseNumber: "",
        apartmentNumber: "",
      },
      description: ""
  } as IEntertainment;

  constructor(private service: EntertainmentService) { }

  public id: string;
  public saved = [];

  ngOnInit() { }

  getById(){
    this.service.getEntertainment(this.id)
    .subscribe((res: IEntertainment) => this.entertainmentInfo = res);
  }

  add() {
    var entertainment : IEntertainment = this.entertainmentInfo;
    this.saved.push(entertainment);
  }

  clear() {
    this.saved = [];
    this.entertainmentInfo = {
      id: "",
      title: "",
      type: "",
      address: {
        street: {
          title: ""
        },
        houseNumber: "",
        apartmentNumber: "",
      },
      description: ""
    }
  }
}