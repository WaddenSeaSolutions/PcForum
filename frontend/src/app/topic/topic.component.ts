import { Component, OnInit } from '@angular/core';
import {firstValueFrom} from "rxjs";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {Thread} from "../../Interface";

@Component({
  selector: 'topic',
  template: `
    <ion-content style="--background: none; top: 20%">
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

  constructor(private http: HttpClient, public service: Service, private router: Router) {
    this.getThreads();
  }
  async getThreads(){
    const call = this.http.get<Thread[]>(environment.baseUrl+'/threads/');
    this.service.threads =await firstValueFrom<Thread[]>(call);
  }

  async openThread(thread: Thread) {
    this.router.navigate(['thread', thread.id])
  }

}
