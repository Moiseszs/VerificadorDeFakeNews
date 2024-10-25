import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { FormsModule } from '@angular/forms';
import { News } from '../../models/news';
import { PostNewsService } from '../../services/post-news.service';
import {
  HttpClient,
  HttpInterceptor,
  HttpInterceptorFn,
} from '@angular/common/http';
import { AsyncPipe, NgFor } from '@angular/common';
import { finalize, Observable } from 'rxjs';
import { MatButton } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatMenuModule } from '@angular/material/menu';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { NgClass, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CheckCardComponent } from '../check-card/check-card.component';
import { Headlines } from '../../models/headlines';
import { MatGridListModule } from '@angular/material/grid-list';

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
    NgIf,
    MatIconModule,
    MatSidenavModule,
    CheckCardComponent,
    MatGridListModule,
  ],
  templateUrl: './news-form.component.html',
  styleUrl: './news-form.component.css',
})
export class NewsFormComponent implements OnInit {
  postService: PostNewsService;
  result$!: Observable<News>;
  headlines$!: Observable<Headlines>;
  isLoading: boolean = false;

  ngOnInit(): void {
    this.headlines$ = this.postService.doGetRelatedHeadlines();
  }

  mediaQuery: MediaQueryList;

  news: News = {
    id: 0,
    keywords: '',
    checkingSources: [],
  };

  async onSubmit() {
    if (this.news.keywords == '') {
      // alert('Insira uma entrada válida, por favor.');
      return;
    }
    this.isLoading = true;
    this.result$ = this.postService.doPost(this.news);
  }

  private _mobileQueryListener: () => void;

  constructor(private http: HttpClient) {
    this.postService = new PostNewsService(http);

    const media = inject(MediaMatcher);
    const changeDetectorRef = inject(ChangeDetectorRef);
    this.mediaQuery = media.matchMedia(
      '(max-width:720px) and (max-height: 1600)'
    );
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mediaQuery.addEventListener('change', this._mobileQueryListener);
  }
}
