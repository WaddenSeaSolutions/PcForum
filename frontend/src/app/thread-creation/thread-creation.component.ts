import { Component, OnInit } from '@angular/core';
import {Thread} from "../../Interface";
import {environment} from "../../environments/environment";
import {FormControl, Validators} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Service} from "../../Service";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastController} from "@ionic/angular";

@Component({
  selector: 'app-thread-creation',
  template: `
    <ion-content style="--background: none; text-align: center; position: absolute; top: 20%">
      <b style="font-size: 250%;">Opret din nye tråd</b>
      <div style="margin-left: 20%; margin-right: 20%; background-color: #1e1e1e; padding: 1%">
        <ion-item >
          <ion-input style="text-align: center; height: 100px; width: 100%; background-color: black" placeholder="Titel" [formControl]="title"></ion-input>
        </ion-item>
        <br>
        <div *ngIf="body.hasError('maxlength')" >
          <p style="color: yellow">Du har overskredet de 2000 maks tegn</p>
        </div>
        <ion-item>
          <ion-textarea style="text-align: center; height: 300px; width: 100%; background-color: black" placeholder="Tekst" [formControl]="body"></ion-textarea>
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

  title = new FormControl('', [Validators.required, Validators.minLength(1),Validators.maxLength(50)]);
  body = new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(2000)]);
  id = new FormControl('')

  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private router: Router, private toastController: ToastController) {

  }

  async createThread() {
    if (this.title.valid && this.body.valid) {
      let topicId = 1;
      this.route.params.subscribe((params) => {
        topicId = params['id']
      });
      const newThread = {
        title: this.title.value,
        body: this.body.value,
        topicId
      };

      let token = localStorage.getItem('token');
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + token
        })
      };
      try {
        await this.http.post<Thread[]>(environment.baseUrl + '/threads/', newThread, httpOptions).toPromise();
        await this.router.navigate(['topic', topicId])
      } catch (e) {
        console.error(e)
      }
    } else {
      this.tellUserNotAllowed()
    }
  }

  async cancel() {
    let topicId = 1;
    this.route.params.subscribe((params) => {
      topicId = params['id']
    });
    await this.router.navigate(['topic', topicId]);
  }

  async tellUserNotAllowed(){
    const toast = await this.toastController.create({
      message: 'Felterne må ikke være tomme. Titel må ikke være over 50 tegn.',
      duration: 2000,
      color: "warning"
    });
    toast.present();
  }

}
