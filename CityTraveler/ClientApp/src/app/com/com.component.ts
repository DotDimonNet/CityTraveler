import { Component, OnInit } from '@angular/core';
import { StatisticService } from 'src/app/services/StatisticService';

@Component({
  selector: 'app-com',
  templateUrl: './com.component.html',
  styleUrls: ['./com.component.css']
})
export class ComComponent implements OnInit{
  public d : number = 2;

  constructor(private service: StatisticService) {}

  ngOnInit() {
    this.service.GetAverageAgeUser()
    .subscribe((res: number) => {
        this.d = res;
    });
  }
}
