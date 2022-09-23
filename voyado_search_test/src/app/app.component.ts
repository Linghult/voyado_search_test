import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SearchResultCountService } from './search-result-count.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public searchResult?: string;
  constructor(private searchResultService: SearchResultCountService) {
  }
  showResult: boolean = false;
  getValue(val: string) {
    if (!!val) {
      this.searchResultService.getData(val).subscribe(data => {
        this.searchResult = data.toString();
        this.showResult = true;
      })
    }
  }

}
