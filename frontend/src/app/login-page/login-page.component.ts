import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";

@Component({
  selector: 'login-page',
  template:
    `
      <div class="background"></div>
      <ion-content style="--background: none; position: absolute; top: 40%">
        <div style="margin-left: 30%; margin-right: 30%">
          <ion-item>
            <ion-input style="text-align: center" placeholder="Brugernavn" [formControl]="username"></ion-input>
          </ion-item>
          <br>
          <ion-item>
            <ion-input style="text-align: center" placeholder="Kodeord" [formControl]="password"></ion-input>
          </ion-item>
          <br>
          <ion-button (click)="login()" style="display: flex">Login</ion-button>
          <br>
          <ion-button (click)="registerNewUser()" style="display: flex">Opret Konto</ion-button>
        </div>
      </ion-content>
    `,
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {
  username = new FormControl('',Validators.compose([Validators.min(5),Validators.max(20)]))
  password = new FormControl('',Validators.compose([Validators.min(8),Validators.max(30),Validators.required]))

  myFormGroup = new FormControl({
    username: this.username,
    password: this.password,
  })
  constructor(private http: HttpClient, private router: Router) { }

async login(){
    if(this.myFormGroup.valid){
const users = {
  username: this.username,
  password: this.password,
};
    const response = this.http.post(environment.baseUrl + '/login', users)

    if(response){
      //Todo go to homepage login
    }
    }
}

 async registerNewUser() {
    await this.router.navigate(['register'])
 }
}
