import { Component } from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";
import {Users} from "../../Interface";

@Component({
  selector: 'login-page',
  template:
    `
      <div class="background"></div>
      <ion-content style="--background: none; position: absolute; top: 40%">
        <div style="margin-left: 20%; margin-right: 20%">
          <ion-item>
            <ion-input style="text-align: center" placeholder="Brugernavn" [formControl]="username"></ion-input>
          </ion-item>
          <br>
          <ion-item>
            <ion-input style="text-align: center" placeholder="Kodeord" [formControl]="password"></ion-input>
          </ion-item>
          <br>
          <div>
          <ion-button class="btnBackground" (click)="login()">Login</ion-button>
          <ion-button class="btnBackground" (click)="registerNewUser()">Opret Konto</ion-button>
          </div>
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

  login() {
    if (this.myFormGroup.valid) {
      const users = {
        username: this.username,
        password: this.password
      };

      this.http.post<any>(environment.baseUrl + '/login', users).subscribe({
        next: (response) => {
          if(response){
            // store your token
            localStorage.setItem('token', response.token);
            //Go to homepage after successful login
            this.router.navigate(["home"])
          }
        },
        error: (err) => {
          // Handle error here
          console.error(err);
        }
      });
    }
  }

 async registerNewUser() {
    await this.router.navigate(['register'])
 }
}
