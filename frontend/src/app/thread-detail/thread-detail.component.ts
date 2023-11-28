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
      <div style="background: #1e1e1e; padding: 2%; margin-left: 10%; margin-right: 10%; border: 1px solid grey" *ngIf="thread">
        <p style="color: white;">Title: {{thread.title}}</p>
        <p style="color: white;">Body: {{thread.body}}</p>
        <ion-button id="DeleteButton">Delete this thread</ion-button>
        <ion-item style="border: 1px solid grey">

        </ion-item>
      </div>
    </ion-content>
  `,
  styleUrls: ['./thread-detail.component.scss'],
})
export class ThreadDetailComponent {
  thread: Thread | undefined;

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getThread();

  }
  async getThread() {
    this.route.params.subscribe((params) => {
      const threadId = params['id'];
      this.http.get<Thread>(`http://localhost:5000/thread/${threadId}`).toPromise()
        .then(
          (response) => {
            console.log(response);
            this.thread = response;
          },
          (error) => {
            console.error('Error fetching thread details:', error);
          }
        );
    });
  }
}
