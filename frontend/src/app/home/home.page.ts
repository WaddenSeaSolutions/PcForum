import { Component } from '@angular/core';
import {Service} from "../../Service";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Topic} from "../../Interface";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-home',
  template: `
    <ion-content style="--background: none">
      <div *ngFor="let topic of service.topics">
        <ion-card>
            <ion-title>Topic title: {{topic.title}}</ion-title>

        </ion-card>
      </div>
    </ion-content>
  ` ,
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private http: HttpClient, public service: Service) {
    this.getTopics();
  }
  async getTopics(){
    const call = this.http.get<Topic[]>(environment.baseUrl+'/topics/');
    const result = await firstValueFrom<Topic[]>(call);
    this.service.topics =result;
  }

}
