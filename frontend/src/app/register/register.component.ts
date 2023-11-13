import { Component} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";

@Component({
  selector: 'register',
  template:
    `

      <ion-content style="--background: none; position: absolute; top: 40%">
      <div style="margin-left: 20%; margin-right: 20%;">
        <header style="text-align: center; font-size: 20px"> Registrer en konto</header>
        <br>
        <ion-item>
    <ion-input style="text-align: center" placeholder="Email" [formControl]="email"> </ion-input>
    </ion-item>
      <br>
      <ion-item>
      <ion-input style="text-align: center" placeholder="Brugernavn" [formControl]="username"> </ion-input>
      </ion-item>
      <br>
      <ion-item>
      <ion-input style="text-align: center" placeholder="Kodeord" [formControl]="password"> </ion-input>
      </ion-item>
      <br>
      <ion-item>
      <ion-input style="text-align: center" placeholder="Gentag Kodeord" [formControl]="password2"> </ion-input>
    </ion-item>
    <br>
        <ion-button style="display: flex">Registrer din konto</ion-button>
      </div>
    </ion-content>
    `,
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent{
  email = new FormControl('',Validators.compose([Validators.min(5),Validators.max(30)]));
  username = new FormControl('',Validators.compose([Validators.min(5),Validators.max(20)]));
  password = new FormControl('',Validators.compose([Validators.min(8),Validators.max(30)]));
  password2 = new FormControl('', Validators.required)

  myFormGroup = new FormControl({
    email: this.email,
    username: this.username,
    password: this.password,
    password2: this.password2
  });

  private passwordMatchValidator(){

  }
  constructor() { }

  async registerUser(){
    const registrant = {
      email: this.email,
      username: this.username,
      password: this.password,
    }


  }
}

export interface UsersRegister {
  email: string;
  username: string;
  password: string;
}
