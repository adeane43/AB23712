import {Component, Inject, OnInit, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MatTableDataSource} from "@angular/material/table";
import {DatePipe} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {interval} from 'rxjs/internal/observable/interval';
import {startWith, switchMap} from 'rxjs/operators';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements OnInit, OnDestroy {
  public dataSource = new MatTableDataSource<WeatherForecast>();
  private datePipe = new DatePipe('en-US');
  private readonly contactId: string;
  private subscription: Subscription;
  public tableColumns: string[] = [
    'date',
    'temperatureC',
    'summary',
    'status'
  ]

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
    this.contactId = route.snapshot.params["contactId"];
    this.subscription = new Subscription();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public ngOnInit(): void {
    this.subscription = interval(500)
      .pipe(
        startWith(0),
        switchMap(() => this.getForecasts())
      )
      .subscribe(res => {
          if (res.body) {
            this.dataSource = new MatTableDataSource<WeatherForecast>(res.body);
          }
        }
      );
  }

  private getForecasts() {
    return this.http.get<WeatherForecast[]>(`${this.baseUrl}/weatherForecast/${this.contactId}`, {observe: "response"});
  }

  private updateForecast(data: WeatherForecast, dataSource: MatTableDataSource<WeatherForecast>) {
    if (!dataSource || !dataSource.data) {
      console.log("No data")
      return;
    }

    let forecast = dataSource.data.find(f => f && f.id && f.id === data.id);

    if (forecast) {
      forecast.summary = data.summary
      forecast.date = data.date;
      forecast.temperatureF = data.temperatureF
      forecast.temperatureC = data.temperatureC;
      forecast.status = data.status;
    }
  }

  public formatDate(date: Date | undefined): string {
    if (!date)
      return '';

    const formatted = this.datePipe.transform(date, 'MM-dd-yyyy HH:mm:ss');
    return formatted || '';
  }
}


export interface WeatherForecast {
  id: number;
  date: Date;
  temperatureC: number;
  temperatureF: number;
  summary: string;
  status: string;
}
