import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {Thread} from "../../Interface";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-thread-detail',
  template: `

  `,
  styleUrls: ['./thread-detail.component.scss'],
})
export class ThreadDetailComponent {

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getThread();

  }
  async getThread() {
    this.route.params.subscribe(async (params) => {
      const threadId = params['id'];

      const call = this.http.get<Thread[]>(`${environment.baseUrl}/thread/${threadId}`);
      this.service.threads = await firstValueFrom<Thread[]>(call);
    });
  }
}
