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
    <ion-content style="--background: none; top: 20%">
      <div style="background: #1e1e1e; padding: 2%; margin-left: 10%; margin-right: 10%; border: 1px solid grey">
        <p style="color: white;">Thread Ejer: {{service.thread?.username}}</p>
        <p style="color: white;">Titel: {{service.thread?.title}}</p>
        <p style="color: white;">Tekst: {{service.thread?.body}}</p>
        <ion-button id="DeleteButton">Delete this thread</ion-button>
        <ion-item style="border: 1px solid grey">

        </ion-item>
      </div>
    </ion-content>
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

      const call = this.http.get<Thread>(`${environment.baseUrl}/thread/${threadId}`);
      this.service.thread = await firstValueFrom<Thread>(call);

    });
  }
}
