import { TemplateParser } from '@angular/compiler';
import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { Observable } from 'rxjs';
import { IEntertainmentPreview } from 'src/app/models/entertainment.preview.model';
import { IEntertainmentShow } from 'src/app/models/entertainment.show.model';
import { IUserProfile } from 'src/app/models/user.model';
import { EntertainmentService } from 'src/app/services/entertainmentService';
import { isIdentifierStart } from 'typescript';

@Component({
  selector: 'entertainment-preview',
  templateUrl: './entertainment.preview.component.html',
  styleUrls: ['./entertainment.preview.component.css']
})
export class EntertainmentPreviewComponent implements OnInit{
  public entertainmentPreview: IEntertainmentPreview = {
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
    description: "",
    tripsCount: 0,
    reviewsCount: 0,
    averageRating: 0,
    imageDTO: {
      sourse: "",
      title: "",
    }
  } as IEntertainmentPreview;

  constructor(private service: EntertainmentService) { }

  public type: number;
  public response = [];

  ngOnInit() { }

  getAll(){
    this.response = [];
    this.service.getAllEntertainment(this.type)
    .subscribe((res: Array<IEntertainmentPreview>) => this.response = res);
  }
}