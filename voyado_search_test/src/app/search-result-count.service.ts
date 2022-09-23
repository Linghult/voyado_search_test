import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class SearchResultCountService {

  constructor(private http: HttpClient) { }
  getData(value: string) {
    let url = 'https://localhost:7247/Search/getResultCount?searchParam='.concat(value);
    let result = this.http.get(url);
    return result;
  }
}
