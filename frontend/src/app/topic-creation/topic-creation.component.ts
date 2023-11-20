import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Service} from "../../Service";
import {Router} from "@angular/router";
import {Topic} from "../../Interface";
import {environment} from "../../environments/environment";
import {FormControl, Validators} from "@angular/forms";


@Component({
  selector: 'app-topic-creation',
  template: `
    <div class="background"></div>
    <ion-content style="--background: none; position: absolute; top: 40%">
      <div style="margin-left: 40%; margin-right: 40%">
        <ion-item>
          <ion-input style="text-align: center" placeholder="Emne titel" [formControl]="title"></ion-input>
        </ion-item>
        <br>
        <ion-item>
          <ion-input style="text-align: center" placeholder="Billede URL" [formControl]="image"></ion-input>
        </ion-item>
        <br>
        <div>
          <ion-button class="btnBackground" (click)="createTopic()">Opret</ion-button>
          <ion-button class="btnBackground" (click)="cancel()">Annuller</ion-button>
        </div>
        </div>
    </ion-content>


  `,
  styleUrls: ['./topic-creation.component.scss'],
})
export class TopicCreationComponent  {

  title = new FormControl('');
  image = new FormControl('');

  constructor(private http: HttpClient, public service: Service, private router: Router) {
  }
  async createTopic(){
    const newTopic = {
      title: this.title.value,
      image:this.image.value
    };
    try {
      await this.http.post<Topic[]>(environment.baseUrl + '/topics/', newTopic).toPromise();
      await this.router.navigate(['home'])
    }
    catch (e){
      console.error(e)
    }
  }
  async cancel(){
    await this.router.navigate(['home'])
  }


}