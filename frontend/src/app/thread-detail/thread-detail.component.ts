import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {Thread} from "../../Interface";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {FormControl, FormGroup} from "@angular/forms";
import {body} from "ionicons/icons";

@Component({
  selector: 'app-thread-detail',
  template: `
    <ion-content style="--background: none; top: 20%">
      <div style="background: #1e1e1e; padding: 2%; margin-left: 10%; margin-right: 10%; border: 1px solid grey">
        <u style="color: white;">Tråd starter: {{service.thread?.username}}</u>
        <p style="color: white;">Titel: {{service.thread?.title}}</p>
        <p style="color: white;">Tekst: {{service.thread?.body}}</p>
        <p> {{getTimeAgo(service.thread?.utctime)}}</p>
        <ion-item>
          <ion-textarea class="styled-textarea" [formControl]="body" placeholder="Enter your comment here..."></ion-textarea>
        </ion-item>
        <ion-button (click)="postComment()">Submit Comment</ion-button>

        <ion-item style="border: 1px solid grey">

        </ion-item>
      </div>
    </ion-content>
  `,
  styleUrls: ['./thread-detail.component.scss'],
})
export class ThreadDetailComponent {

  body = new FormControl('');

  myFormgroup = new FormGroup({
    body: this.body
  })

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getThread();
  }

  async getThread() {
    this.route.params.subscribe(async (params) => {
      const threadId = params['id'];
      const threadCall = this.http.get<Thread>(`${environment.baseUrl}/thread/${threadId}`);
      this.service.thread = await firstValueFrom<Thread>(threadCall);
    });
  }

  async postComment() {
    this.route.params.subscribe(async (params) => {
      const threadId = params['id'];

      // Get the token from local storage.
      const token = localStorage.getItem('YourTokenKey');

      // Create headers and add the Authorization header.
      const httpOptions = {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${token}`
        })
      };

      // Use httpOptions and commentData in your HTTP call.
      const commentCall = this.http.post<Comment>(`${environment.baseUrl}/comment/${threadId}`, this.body.value, httpOptions);
      this.service.comment = await firstValueFrom<Comment>(commentCall);
    });
  }

  getTimeAgo(timeStamp: string | undefined): string {
    if (!timeStamp) {
      return 'Henter tidsstempel';
    }

    const oldDate = new Date(timeStamp);
    const currentDate = new Date();

    const secondsPassed = Math.floor((currentDate.getTime() - oldDate.getTime()) / 1000);

    if (secondsPassed < 60) {
      return `${secondsPassed} sekunder siden`;
    }

    const minutesPassed = Math.floor(secondsPassed / 60);

    if (minutesPassed < 60) {
      return minutesPassed === 1 ? '1 minut siden' : `${minutesPassed} minutter siden`;
    }

    const hoursPassed = Math.floor(minutesPassed / 60);

    if (hoursPassed < 24) {
      return hoursPassed === 1 ? '1 time siden' : `${hoursPassed} timer siden`;
    }

    const daysPassed = Math.floor(hoursPassed / 24);

    return daysPassed === 1 ? '1 dag siden' : `${daysPassed} dage siden`;
  }

}
