import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { News } from '../../models/news';
import { PostNewsService } from '../../services/post-news.service';
import { HttpClient } from '@angular/common/http';
import { AsyncPipe, NgFor } from '@angular/common';
import { Observable } from 'rxjs';
import { MatButton } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatMenuModule } from '@angular/material/menu';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { NgClass } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';

@Component({
  selector: 'app-news-form',
  standalone: true,
  imports: [
    FormsModule,
    NgFor,
    AsyncPipe,
    MatButton,
    MatInputModule,
    MatFormFieldModule,
    FlexLayoutModule,
    MatMenuModule,
    MatCardModule,
    MatChipsModule,
    NgClass,
    MatIconModule,
    MatSidenavModule,
  ],
  templateUrl: './news-form.component.html',
  styleUrl: './news-form.component.css',
})
export class NewsFormComponent {
  postService: PostNewsService;

  news: News = {
    id: 0,
    keywords: '',
    checkingSources: [],
  };

  result$!: Observable<News>;

  async onSubmit() {
    this.result$ = this.postService.doPost(this.news);
  }

  constructor(private http: HttpClient) {
    this.postService = new PostNewsService(http);
  }

  checkVeridictColors(veridict: string): string {
    if (veridict === 'enganoso') {
      return 'red';
    } else if (veridict === 'distorcido') {
      return 'orange';
    } else if (veridict === 'indefindo') {
      return 'grey';
    }

    return '';
  }
}
