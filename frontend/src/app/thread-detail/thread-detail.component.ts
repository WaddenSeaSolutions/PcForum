import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {Thread, Topic, UserComment} from "../../Interface";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {body} from "ionicons/icons";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-thread-detail',
  template: `
    <ion-content style="--background: none; top: 20%;">
      <div style="background: #1e1e1e; padding: 1%; margin-left: 5%; margin-right: 5%; border: 1px solid grey; overflow: auto; height: 78%" *ngIf="service.thread; else noThreadTemplate">
        <div style="text-align: center">
        <ion-title style="color: white; ">{{service.thread.title}}</ion-title>
        </div>
        <div style="border-bottom: 2px solid grey; width: 100%">
          <br>
        <u style="color: white;">Tråd starter: {{service.thread.username}}</u>
          <br>
          <ion-button color="danger" *ngIf="checkIfAdmin" (click)="banUser(service.thread.username)">Bandlys bruger</ion-button>
        <p> {{getTimeAgo(service.thread.utctime)}}</p>
        </div>
        <p style="color: white;" [innerHTML]="service.thread ? extractAndDisplayImages(service.thread.body) : ''"></p>
        <ion-button color="danger" *ngIf="checkIfAdmin" (click)="deleteThread(service.thread.id)">Slet Tråd</ion-button>
        <ion-item *ngIf="checkIfLoggedIn">
          <ion-textarea class="styled-textarea" [formControl]="body"
                        placeholder="Indsæt din kommentar her...."></ion-textarea>
        </ion-item>
        <ion-button *ngIf="checkIfLoggedIn" (click)="postComment()">Submit Comment</ion-button>

        <b style="margin-top: 2%; color: yellow" *ngIf="!checkIfLoggedIn"> Du skal være logget ind for, at kunne kommenterer</b>
        <div *ngFor="let userComment of service.userComments">
          <ion-item style="border: 1px solid grey;">
              <div style="padding: 2%; border-right: 2px solid grey; width: 15%; height: 100%">
                <u>{{userComment.username}}</u>
                <p>{{getTimeAgo(userComment.utctime)}}</p>
                <ion-button *ngIf="checkIfAdmin" color="danger" style=" cursor: pointer; " (click)="deleteComment(userComment.id)">Delete comment</ion-button>
              </div>
              <div style="display: flex; flex-wrap: wrap; margin-left: 1%;">
              <p [innerHtml]="extractAndDisplayImages(userComment.body)"></p>
            </div>
          </ion-item>
        </div>
      </div>
      <ng-template #noThreadTemplate>
        <div>
          <ion-title>Denne tråd findes ikke</ion-title>
        </div>
      </ng-template>
    </ion-content>
  `,
  styleUrls: ['./thread-detail.component.scss'],
})
export class ThreadDetailComponent {
  public checkIfAdmin: boolean;
  public checkIfLoggedIn: boolean;

  body = new FormControl('',[Validators.required, Validators.minLength(1), Validators.maxLength(2000)]);

  myFormgroup = new FormGroup({
    body: this.body
  })

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute) {
    this.service.thread = null;
    this.service.userComments = [];
    this.checkIfLoggedIn = localStorage.getItem('token') !== null
    this.checkIfAdmin = localStorage.getItem('role') === 'admin';
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

      const commentCall = this.http.post<UserComment>(`${environment.baseUrl}/comment/${threadId}`,
        this.myFormgroup.value, httpOptions);
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
  /*
   A method that recieves a timestamp and uses it to calculate the time since it was written
   */

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

  /*
  Method to extract images from text and display them and the text accordingly to how it was written.
  Used instead of image input
   */
  extractAndDisplayImages(text: string) : any {
    let updatedText = text;

    const urlPattern = /(https?:\/\/[^\s]+)/g;  // Matches urls
    const matchedUrls = text.match(urlPattern);

    if (matchedUrls) {
      matchedUrls.forEach(url => {
        updatedText = updatedText.replace(url, `<br> <img src="${url}" alt="Ikke tilgængeligt billede"> <br>`);
      });
    }

    const paragraphs = updatedText.split(/\n{1,}/);  // Splits the text into paragraphs at every two consecutive line breaks
    updatedText = paragraphs.map(paragraph => `<p>${paragraph}</p>`).join('');

    return updatedText;
  }

  protected readonly console = console;

  async deleteComment(id: number) {
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
    const softDeleteUrl = `${environment.baseUrl}/comment/${id}`;

    try {
      await this.http.put(softDeleteUrl, { deleted: true }, httpOptions).toPromise();

       this.getComments();
    } catch (error) {
      console.error(error);
    }
  }

  async deleteThread(id: number) {
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
    const softDeleteUrl = `${environment.baseUrl}/thread/${id}`;
    try {
      await this.http.put(softDeleteUrl, { deleted: true }, httpOptions).toPromise();

      location.reload();
    } catch (error) {
      console.error(error);
    }
  }

  async banUser(username: string) {
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
    const softDeleteUrl = `${environment.baseUrl}/user`;
    let payload = { username: username };
    try {
      const response = await this.http.put(softDeleteUrl, payload,httpOptions).toPromise();
    }
    catch (error){
      console.error(error)
    }
  }
}
