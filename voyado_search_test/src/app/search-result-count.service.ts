import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class SearchResultCountService {

  constructor(private http: HttpClient) { }
  getData(searchQuery: string) {
    let url = 'https://localhost:7247/Search/getResultCount?searchParam='.concat(searchQuery);
    let result = this.http.get(url);
    return result;
  }
}
