import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Service} from "../../Service";
import {Router} from "@angular/router";
import {Topic} from "../../Interface";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-topic-moderation',
  template: `
    <ion-content style="--background: none; position: absolute; top: 15%">
      <div *ngFor="let topic of service.topics">
        <ion-card id="topicCards">
          <ion-img id="topicImage" src="{{topic.image}}"></ion-img>
          <ion-title id="topicTitle" (click)="openTopic(topic)">{{topic.title}}</ion-title>
          <ion-button color="danger" style=" cursor: pointer; " (click)="deleteTopic(topic)"> Slet emne </ion-button>
          <ion-button style="color: white; cursor: pointer; --background: none;" (click)="openUpdateTopic(topic)"> Ændre emne </ion-button>
          <p>Antal af tråde: </p>
        </ion-card>
      </div>
    </ion-content>
  `,
  styleUrls: ['./topic-moderation.component.scss'],
})
export class TopicModerationComponent {

  constructor(private http: HttpClient, public service: Service, private router: Router) {
    this.getTopics();
  }

  async getTopics(){
    const call = this.http.get<Topic[]>(environment.baseUrl+'/topics/');
    this.service.topics =await firstValueFrom<Topic[]>(call);
  }

  async openTopic(topic: Topic){

  }

  async deleteTopic(topic: Topic) {
    const deleteUrl = `${environment.baseUrl}/topics/${topic.id}`; // Assuming topic has an 'id' property

    try {
      await this.http.delete(deleteUrl).toPromise();
      const call = this.http.get<Topic[]>(environment.baseUrl+'/topics/');
      this.service.topics =await firstValueFrom<Topic[]>(call);
    } catch (error) {
      // Handle errors - log or notify the user about the failure
    }
  }

  async openUpdateTopic(topic: Topic) {

  }
}
