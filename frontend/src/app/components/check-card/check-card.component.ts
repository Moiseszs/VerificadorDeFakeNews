import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { NgClass, NgFor } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { News } from '../../models/news';

@Component({
  selector: 'app-check-card',
  standalone: true,
  imports: [
    MatMenuModule,
    MatCardModule,
    MatChipsModule,
    MatIconModule,
    NgClass,
    NgFor,
    FlexLayoutModule,
  ],
  templateUrl: './check-card.component.html',
  styleUrl: './check-card.component.css',
})
export class CheckCardComponent {
  @Input() result?: News | null;

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
