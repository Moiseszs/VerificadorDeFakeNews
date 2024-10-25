import { Injectable } from '@angular/core';
import { News } from '../models/news';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { Headlines } from '../models/headlines';
@Injectable({
  providedIn: 'root',
})
export class PostNewsService {
  constructor(private http: HttpClient) {}

  result: News = {
    id: 0,
    keywords: '',
    checkingSources: [],
  };

  doPost(news: News): Observable<News> {
    return this.http.post<News>(environment.apiUrl, news);
  }

  doGetRelatedHeadlines(): Observable<Headlines> {
    return this.http.get<Headlines>(environment.apiUrl + 'related-news');
  }
}
