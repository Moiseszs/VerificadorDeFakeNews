import { TestBed } from '@angular/core/testing';

import { PostNewsService } from './post-news.service';

describe('PostNewsService', () => {
  let service: PostNewsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostNewsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
