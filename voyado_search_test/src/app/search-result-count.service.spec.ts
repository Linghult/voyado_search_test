import { TestBed } from '@angular/core/testing';

import { SearchResultCountService } from './search-result-count.service';

describe('SearchResultCountService', () => {
  let service: SearchResultCountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearchResultCountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
