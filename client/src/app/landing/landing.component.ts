import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule],
  template: ` {{ users$ | async | json }} `,
  styles: ``,
})
export class LandingComponent {
  private http = inject(HttpClient);
  public readonly users$ = this.http.get(`${environment.api}/users`);
}
