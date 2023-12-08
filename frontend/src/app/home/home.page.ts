import {Component} from '@angular/core';
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Topic} from "../../Interface";
import {firstValueFrom} from "rxjs";
import {Router} from "@angular/router";
import {navigate} from "ionicons/icons";

@Component({
  selector: 'app-home',
  template: `
    <ion-content style="--background: none; position: absolute; top: 15%">
      <ion-card id ="topicCards" *ngIf="checkIfAdmin">
        <ion-title>Modereringskontrol:</ion-title>
        <ion-button (click)="openCreateTopic()" style="--background: none;">Opret nyt emne</ion-button>
        <ion-button (click)="openTopicModeration()" style="--background: none;">Administrere emner</ion-button>
      </ion-card>
      <div *ngFor="let topic of service.topics">
        <ion-card id="topicCards">
          <ion-img id="topicImage" src="{{topic.image}}"></ion-img>
            <ion-title id="topicTitle" (click)="openTopic(topic)">{{topic.title}}</ion-title>
        </ion-card>
      </div>
    </ion-content>
  ` ,
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  public checkIfAdmin: boolean;

  constructor(private http: HttpClient, public service: Service, private router: Router) {
    //Checks if the user is an admin role, if not the user should not be shown the admin l
    this.checkIfAdmin = localStorage.getItem('role') === 'admin';
    this.getTopics();
  }
  async getTopics(){
    const call = this.http.get<Topic[]>(environment.baseUrl+'/topics/');
    this.service.topics =await firstValueFrom<Topic[]>(call);
  }

  async openTopic(topic: Topic){
    this.router.navigate(['topic', topic.id])
  }

  async openCreateTopic(){
    this.router.navigate(['topic-creation'])
  }

  async openTopicModeration(){
    this.router.navigate(['topic-moderation'])
  }

  protected readonly localStorage = localStorage;
}
