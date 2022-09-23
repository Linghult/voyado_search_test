import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SearchResultCountService } from './search-result-count.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public forecasts?: WeatherForecast[];
  public searchCount?: SearchResultCount;
  public searchServiceResult?: string;
  public searchResult?: string;
  constructor(/*http: HttpClient*/ private searchResultService: SearchResultCountService) {
    //this.searchResultService.getData().subscribe(data => {
    //  console.warn(data)
    //})
    //http.get<SearchResultCount>('/SearchResultCount').subscribe(result => {
    //  this.searchCount = result;
    //}, error => console.error(error));
    //http.get<WeatherForecast[]>('/weatherforecast').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
  }
  //Kanske göra till int

  getValue(val: string) {
    //let result = this.searchResultService.getData(val);
    //console.warn(result)
    this.searchResultService.getData(val).subscribe(data => {
      this.searchResult = data.toString();
    })
  }
}



interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface SearchResultCount {
  totalCount: number;
}
