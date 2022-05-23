import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {HubService} from "../services/hub-service";
import {MatTableDataSource} from "@angular/material/table";
import {DatePipe} from "@angular/common";
import {ActivatedRoute, ActivatedRouteSnapshot} from "@angular/router";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements OnInit {
  public dataSource = new MatTableDataSource<WeatherForecast>();
  private datePipe = new DatePipe('en-US');
  private readonly contactId: string;
  public tableColumns: string[] = [
    'date',
    'temperatureC',
    'summary',
    'status'
  ]

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private hubService: HubService, private route: ActivatedRoute) {
    this.contactId = route.snapshot.params["contactId"];
  }

  public ngOnInit(): void {
    this.hubService.startConnection(this.contactId);

    this.http.get<WeatherForecast[]>(`${this.baseUrl}/weatherForecast/${this.contactId}`, {observe: "response"})
      .subscribe(data => {
        if (data.body) {
          this.dataSource = new MatTableDataSource<WeatherForecast>(data.body);
          this.hubService.addHubListener(data => this.updateForecast(data, this.dataSource));
        }
      });
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
