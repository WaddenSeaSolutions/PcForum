import { Component, OnInit } from '@angular/core';
import {ToastController} from "@ionic/angular";
import {Thread, Users, UserComment} from "../../Interface";
import {Service} from "../../Service";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {comment} from "postcss";

@Component({
  selector: 'app-profile',
  template: `
    <ion-content style="--background: none; position: absolute; top: 15%">
      <ion-card id="cards" *ngIf="checkIfLoggedIn">


        <ion-card-content>
          <ion-list>
            <ion-item>
                <ion-text>Email</ion-text>

            </ion-item>
            <ion-item>
              <ion-text>{{service.users?.email}}</ion-text>
            </ion-item>
            <ion-item>
                <ion-text>Brugernavn</ion-text>
            </ion-item>
            <ion-item>
              <ion-text>{{service.users?.username}}</ion-text>
            </ion-item>
          </ion-list>
        </ion-card-content>
        <ion-list>
          <ion-item>
        <ion-text>Mine tråde</ion-text>
          </ion-item>
          <div style="height: 80%; overflow-y: scroll;">
          <div *ngFor="let thread of service.threads">
          <ion-item>
              <ion-card>
          <u (click)="navigateToThread(thread.id)" style="cursor: pointer;">{{thread.title}} - Comments:</u>
              </ion-card>
            </ion-item>
          </div>
          </div>
          </ion-list>
        <ion-list>
            <ion-item>
              <ion-text>Mine kommentarer</ion-text>
            </ion-item>
          <div style="height: 80%; overflow-y: scroll;">
            <div *ngFor="let userComment of service.userComments">
              <ion-item>
                <ion-card>
                  <u (click)="navigateToComment(userComment.threadId)" style="cursor: pointer;">{{userComment.body}}</u>
                </ion-card>
              </ion-item>
            </div>
          </div>
        </ion-list>
      </ion-card>
    </ion-content>
  `,
  styleUrls: ['./profile.component.scss'],
})

export class ProfileComponent{


  public checkIfLoggedIn: boolean;
  constructor(private http: HttpClient, public service: Service, private route: ActivatedRoute, private toastController: ToastController, private router: Router) {
    this.checkIfLoggedIn = localStorage.getItem('token') != null;
    if (!this.checkIfLoggedIn){
      this.tellUserToLogin();
      this.router.navigate(['home']);
    }else {
      this.getThreads()
      this.getUserInformation()
      this.getUserComments()
    }

  }

  async getThreads() {
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
      const call = this.http.get<Thread[]>(`${environment.baseUrl}/profile/`, httpOptions);
      this.service.threads = await firstValueFrom<Thread[]>(call);
  }

  async getUserComments(){
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
    const call = this.http.get<UserComment[]>(`${environment.baseUrl}/usercomments`, httpOptions);
    this.service.userComments = await firstValueFrom<UserComment[]>(call)
  }


  async tellUserToLogin(){
    const toast = await this.toastController.create({
      message: 'Du er nødt til at logge ind for at bruge profil siden.',
      duration: 4000
    });
    toast.present();
  }

  async navigateToThread(threadId: number){
    this.router.navigate(["thread", threadId])
  }

  async navigateToComment(threadId: number){
    this.router.navigate(["thread", threadId])
  }

  async getUserInformation(){
    let token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      })
    };
    this.http.get(`${environment.baseUrl}/userprofile/`, httpOptions).toPromise().then(
      (response: any) => {   // using any type here to bypass TypeScript strict type checking.
        this.service.users = response;
      }
    ).catch(
      (error) => {console.error('En fejl skete ved afhenting af bruger information', error)}
    );
  }

  protected readonly comment = comment;
}
