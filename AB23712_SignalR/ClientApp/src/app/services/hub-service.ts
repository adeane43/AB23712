import {Inject, Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"
import {WeatherForecast} from "../fetch-data/fetch-data.component";

@Injectable({
  providedIn: 'root'
})
export class HubService {
  private hubConnection: signalR.HubConnection | undefined;

  constructor(@Inject('BASE_URL') private baseUrl: string) {
  }

  public startConnection = (contactId: string) => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hub/connect?contactId=${contactId}`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addHubListener = (callback : (data : WeatherForecast) => void) => {
    if (!this.hubConnection)
      return;

    this.hubConnection.on('ForecastChanged', data => {
      callback(data);
      console.log(data);
    });
  }
}
