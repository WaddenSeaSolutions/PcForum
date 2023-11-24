import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";
import {Users} from "../../Interface";

@Component({
  selector: 'login-page',
  template:
    `
      <div class="background"></div>

      <ion-content style="--background: none; position: absolute; top: 20%">
        <div style="margin-left: 25%; margin-right: 25%; background-color: #1E1E1E; padding: 2%; border: 1px solid grey ; text-align: center; ">
          <strong><p>Login side</p></strong>
          <ion-item>
            <br>
            <ion-input style="text-align: center; background-color: #121212" placeholder="Brugernavn" [formControl]="username"></ion-input>
          </ion-item>
          <br>
          <ion-item>
            <ion-input style="text-align: center; background-color: #121212" placeholder="Kodeord" [formControl]="password"></ion-input>
          </ion-item>
          <br>
          <div style="display: flex; justify-content: center;">
            <ion-button class="btnBackground" style="flex: 1; margin: 3%" (click)="login()">Login</ion-button>
            <ion-button class="btnBackground" style="flex: 1; margin: 3%" (click)="registerNewUser()">Opret Konto</ion-button>
          </div>
        </div>
      </ion-content>
    `,
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {
  username = new FormControl('',Validators.compose([Validators.min(5),Validators.max(20)]))
  password = new FormControl('',Validators.compose([Validators.min(8),Validators.max(30),Validators.required]))

  myFormGroup = new FormGroup({
    username: this.username,
    password: this.password,
  })
  constructor(private http: HttpClient, private router: Router) { }

  login() {
    if (this.myFormGroup.valid || true) {
      this.http.post(environment.baseUrl + '/login', this.myFormGroup.value, {responseType: 'text'})
        .subscribe({
          next: (response) => {
            if(response){
              // store token
              localStorage.setItem('token', response);
              let payload = JSON.parse(atob(response.split(".")[1]))
              //Store the role, only allows for visual admin controls
              localStorage.setItem('role',payload.role)
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
