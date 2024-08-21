import { Injectable } from '@angular/core';
import { News } from '../models/news';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PostNewsService {
  url = 'https://verificadordefakenews.azurewebsites.net/api/news';
  localUrl = 'https://localhost:7297/api/news';

  constructor(private http: HttpClient) {}

  result: News = {
    id: 0,
    title: '',
    checkingSources: [],
  };

  doPost(news: News): Observable<News> {
    return this.http.post<News>(this.localUrl, news);
  }

  async Get() {
    return this.http
      .get<string>(`${this.url}/api/news`)
      .subscribe((data) => console.log(data));
  }
}
