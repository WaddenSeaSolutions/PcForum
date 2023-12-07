import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {Thread, UserComment} from "../../Interface";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {FormControl, FormGroup} from "@angular/forms";
import {body} from "ionicons/icons";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-thread-detail',
  template: `
    <ion-content style="--background: none; top: 20%;">
      <div
        style="background: #1e1e1e; padding: 1%; margin-left: 5%; margin-right: 5%; border: 1px solid grey; overflow: auto; height: 78%">
        <u style="color: white;">Tråd starter: {{service.thread?.username}}</u>
        <p style="color: white;">Titel: {{service.thread?.title}}</p>
        <p style="color: white;">Tekst: {{service.thread?.body}}</p>
        <p> {{getTimeAgo(service.thread?.utctime)}}</p>
        <ion-item>
          <ion-textarea class="styled-textarea" [formControl]="body"
                        placeholder="Indsæt din kommentar her...."></ion-textarea>
        </ion-item>
        <ion-button (click)="postComment()">Submit Comment</ion-button>

        <div *ngFor="let userComment of service.userComments">
          <ion-item style="border: 1px solid grey">
            <div style="display: flex; flex-wrap: wrap; align-items: flex-start;">
              <div>
                <p>{{userComment.username}}</p>
              </div>
              <p [innerHtml]="extractAndDisplayImages(userComment.body)"> {{userComment.username}}</p>
            </div>
          </ion-item>
        </div>
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

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router, private sanitizer: DomSanitizer) {
    this.getThread();
    this.getComments();
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

      let token = localStorage.getItem('token');
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token
        })
      };

      const commentCall = this.http.post<UserComment>(`${environment.baseUrl}/comment/${threadId}`, this.myFormgroup.value, httpOptions);
      this.service.userComment = await firstValueFrom<UserComment>(commentCall);

      // Refresh comments after posting
      this.getComments();
      this.body.reset();
    });
  }

  async getComments(){
    this.route.params.subscribe(async (params) => {
      const threadId = params['id'];
    const call = this.http.get<UserComment[]>(`${environment.baseUrl}/getComment/${threadId}`);
    this.service.userComments = await firstValueFrom<UserComment[]>(call)
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

  extractAndDisplayImages(text: string) : any {
    let updatedText = text;
    const urlPattern = /(https?:\/\/[^\s]+)/g;
    const matchedUrls = text.match(urlPattern);
    if (matchedUrls) {
      matchedUrls.forEach(url => {
        updatedText = updatedText.replace(url, `<br> <img src="${url}" alt="Billedet kunne ikke vises"> <br>`);
      });
    }
    return updatedText;
  }

  protected readonly console = console;
}
