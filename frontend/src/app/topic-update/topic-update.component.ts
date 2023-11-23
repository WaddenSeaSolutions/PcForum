import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {Topic} from "../../Interface";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-topic-update',
  template: `
    <div *ngIf="topic" class="background"></div>
    <ion-content style="--background: none; position: absolute; top: 40%">
      <div style="margin-left: 40%; margin-right: 40%">
        <ion-item>
          <ion-input style="text-align: center" [formControl]="title"></ion-input>
        </ion-item>
        <br>
        <ion-item>
          <ion-input style="text-align: center" [formControl]="image"></ion-input>
        </ion-item>
        <br>
        <div>
          <ion-button class="btnBackground" (click)="updateTopic()">Godkend</ion-button>
          <ion-button class="btnBackground" (click)="cancel()">Annuller</ion-button>
        </div>
      </div>
    </ion-content>
  `,
  styleUrls: ['./topic-update.component.scss'],
})
export class TopicUpdateComponent {

  title = new FormControl('');
  image = new FormControl('');
  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router) {
    this.getBoxData();
  }
  topic: Topic | undefined
  myFormGroup = new FormGroup({
    title: this.title,
    image: this.image
  })

  async getBoxData(){
    this.route.params.subscribe((params) => {
      const topicId = params['id']; // Get the 'id' parameter from the route
      this.http.get<Topic>(`${environment.baseUrl}/topic-update/${topicId}`).toPromise()
        .then(
          (response) => {
            this.topic = response;
            this.title.setValue(this.topic!.title);
            this.image.setValue(this.topic!.image);
          },
          (error) => {
            console.error('Error fetching topic details:', error);
          }
        );
    });
  }


  async updateTopic() {
      const newTopic = this.myFormGroup.value as Topic;
    try {
      await this.http.put<Topic[]>(environment.baseUrl + `/topic-update/${this.topic?.id}`, newTopic).toPromise();
      await this.router.navigate(['topic-moderation'])
    }
    catch (e){
      console.error(e)
    }
  }

  async cancel() {
    await this.router.navigate(['topic-moderation'])
  }
}
