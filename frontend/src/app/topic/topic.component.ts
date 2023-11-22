import { Component, OnInit } from '@angular/core';
import {firstValueFrom} from "rxjs";
import {environment} from "../../environments/environment";
import {ActivatedRoute, Router} from "@angular/router";
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {Thread} from "../../Interface";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'topic',
  template: `
    <ion-content style="--background: none; top: 20%">
      <ion-card>
        <ion-button class="btnBackground" (click)="createNewThread()" style="background: none;">Opret</ion-button>
      </ion-card>
      <div *ngFor="let thread of service.threads">
        <ion-card id="threadCard">
         <ion-title (click)="openThread(thread)" style="color: white; cursor: pointer"> {{thread.title}} </ion-title>
        </ion-card>
      </div>

    </ion-content>




  `,
  styleUrls: ['./topic.component.scss'],
})
export class TopicComponent {

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getThreads();
  }

  async getThreads() {

    const call = this.http.get<Thread[]>(environment.baseUrl + '/threads/', topicId);
    this.service.threads = await firstValueFrom<Thread[]>(call);
  }

  async openThread(thread: Thread) {
    this.router.navigate(['thread', thread.id])
  }

  async createNewThread() {
    this.route.params.subscribe((params) => {
      const topicId = params['id']
    this.router.navigate(['thread-creation', topicId ])});
  }



}
