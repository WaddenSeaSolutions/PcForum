import { Component, OnInit } from '@angular/core';
import {ToastController} from "@ionic/angular";
import {Thread, Users} from "../../Interface";
import {Service} from "../../Service";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-profile',
  template: `
    <ion-content style="--background: none; position: absolute; top: 15%">
      <ion-card id="cards" *ngIf="checkIfLoggedIn">


<!--        <ion-card-content *ngIf="user">-->
        <ion-card-content>
          <ion-list>
            <ion-item>
                <ion-text>Email</ion-text>
<!--                <ion-text>{{user.email}}</ion-text>-->
            </ion-item>
            <ion-item>
                <ion-input id="inputs">Brugernavn</ion-input>
            </ion-item>
            <ion-button> rofl</ion-button>
          </ion-list>
        </ion-card-content>
        <ion-list>
          <ion-item>
        <ion-text>Mine tråde</ion-text>
          </ion-item>
          <div *ngFor="let thread of service.threads">
          <ion-item>
            <ion-button>
          <ion-text>{{thread.title}}</ion-text>
            </ion-button>
            </ion-item>
          </div>
          </ion-list>
      </ion-card>
    </ion-content>
  `,
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  user: Users | undefined;

  public checkIfLoggedIn: boolean;
  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private toastController: ToastController) {
    this.checkIfLoggedIn = localStorage.getItem('token') != null;
    console.log(localStorage.getItem('nameid'))
    this.getThreads()
  }

  async getThreads() {
    this.route.params.subscribe(async (params) => {
      const userId = params['id'];

      const call = this.http.get<Thread[]>(`${environment.baseUrl}/profile/${userId}`);
      this.service.threads = await firstValueFrom<Thread[]>(call);
    });
  }


  async tellUserToLogin(){
    const toast = await this.toastController.create({
      message: 'Du er nødt til at logge ind for at bruge profil siden.',
      duration: 4000
    });
    toast.present();
  }

  async getUserInformation(){

  }
}
