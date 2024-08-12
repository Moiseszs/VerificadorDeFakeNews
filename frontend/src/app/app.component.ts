import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NewsFormComponent } from './components/news-form/news-form.component';
import { MatMenu } from '@angular/material/menu';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NewsFormComponent,
    MatMenu,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontend';
}
