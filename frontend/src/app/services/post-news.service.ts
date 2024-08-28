import { Injectable } from '@angular/core';
import { News } from '../models/news';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
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

  async Get() {
    return this.http
      .get<string>(`${environment.apiUrl}/api/news`)
      .subscribe((data) => console.log(data));
  }
}
