import { Component, OnInit } from '@angular/core';
import { EntertainmentService } from 'src/app/services/entertainmentService';

@Component({
  selector: 'find-area',
  templateUrl: './findArea.component.html',
  styleUrls: ['./findArea.component.css']
})
export class FindArea implements OnInit{

  constructor() { }

  public buttons = ["All", "Title", "Street", "ID"];

  ngOnInit() { }
}