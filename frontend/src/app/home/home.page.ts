import {Component} from '@angular/core';
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Topic} from "../../Interface";
import {firstValueFrom} from "rxjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  template: `
    <ion-content style="--background: none; position: absolute; top: 15%">
      <div *ngFor="let topic of service.topics">
        <ion-card id="topicCards">
            <ion-title (click)="openTopic(topic)" style="color: white; cursor: pointer">{{topic.title}}</ion-title>

        </ion-card>
      </div>
    </ion-content>
  ` ,
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private http: HttpClient, public service: Service, private router: Router) {
    this.getTopics();
  }
  async getTopics(){
    const call = this.http.get<Topic[]>(environment.baseUrl+'/topics/');
    this.service.topics =await firstValueFrom<Topic[]>(call);
  }

  async openTopic(topic: Topic){
    this.router.navigate(['topic', topic.id])
  }

}
