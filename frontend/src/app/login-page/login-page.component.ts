import { Component, OnInit } from '@angular/core';
import {FormControl} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-login-page',
  template:
    `
    <ion-input placeholder="Username" [ngModel]="username"></ion-input>
    <ion-input placeholder="Password" [ngModel]="password"></ion-input>
    <ion-button (click)="login()">Login</ion-button>
    `,
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent {
  username = new FormControl('')
  password = new FormControl('')

  myFormGroup = new FormControl({
    username: this.username,
    password: this.password,
  })
  constructor(private http: HttpClient) { }

async login(){
   // const call = this.http.post()
}
}
