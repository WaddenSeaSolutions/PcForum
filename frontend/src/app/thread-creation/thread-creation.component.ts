import { Component, OnInit } from '@angular/core';
import {Thread} from "../../Interface";
import {environment} from "../../environments/environment";
import {FormControl} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Service} from "../../Service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-thread-creation',
  template: `
    <div class="background"></div>
    <ion-content style="--background: none; position: absolute; top: 40%">
      <div style="margin-left: 40%; margin-right: 40%">
        <ion-item>
          <ion-input style="text-align: center" placeholder="Titel" [formControl]="title"></ion-input>
        </ion-item>
        <br>
        <ion-item>
          <ion-input style="text-align: center" placeholder="Tekst" [formControl]="body"></ion-input>
        </ion-item>
        <br>

        <div>
          <ion-button class="btnBackground" (click)="createThread()">Opret</ion-button>
          <ion-button class="btnBackground" (click)="cancel()">Annuller</ion-button>
        </div>
      </div>
    </ion-content>


  `,
  styleUrls: ['./thread-creation.component.scss'],
})
export class ThreadCreationComponent {

  title = new FormControl('');
  body = new FormControl('');
  id = new FormControl('')

  constructor(private http: HttpClient, public service: Service, private router: Router) {

  }

  async createThread() {
    const newThread = {
      title: this.title.value,
      body: this.body.value,
      id: this.id.value
    };
    try {
      await this.http.post<Thread[]>(environment.baseUrl + 'threads', newThread).toPromise();
      await this.router.navigate(['topics'])
    }
    catch (e) {
      console.error(e)
    }
  }

  async cancel() {
    await this.router.navigate(['topics'])
  }

}
