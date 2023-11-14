import { Component} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";



@Component({
  selector: 'register',
  template:
    `<div class="background"></div>
    <ion-content style="--background: none; position: absolute; top: 40%">
        <div style="margin-left: 20%; margin-right: 20%;">
          <header style="text-align: center; font-size: 20px"> Registrer en konto</header>
          <br>
          <ion-item>
            <ion-input style="text-align: center" placeholder="Email" [formControl]="email"> </ion-input>
          </ion-item>
          <div *ngIf="email.hasError('minlength')">
            <p>Email skal være over 3 tegn</p>
          </div>
          <div *ngIf="email.hasError('maxlength')">
            <p>Email skal være under 30 tegn</p>
          </div>
          <br>

          <ion-item>
            <ion-input style="text-align: center" placeholder="Brugernavn" [formControl]="username"> </ion-input>
          </ion-item>
          <div *ngIf="username.hasError('minlength')">
            <p>Brugernavn skal være mindst 5 tegn</p>
          </div>
          <div *ngIf="username.hasError('maxlength')">
            <p>Brugernavn skal være højest 20 tegn</p>
          </div>

          <br>
          <ion-item>
            <ion-input type="password" style="text-align: center" placeholder="Kodeord" [formControl]="password"> </ion-input>
          </ion-item>
          <br>
          <ion-item>
            <ion-input type="password" style="text-align: center" placeholder="Gentag Kodeord" [formControl]="password2"> </ion-input>
          </ion-item>
          <div *ngIf="password2.hasError('passwordsNotMatch')">
            <p>Passwords do not match</p>
          </div>
          <br>
          <ion-button class="btnBackground" style="display: flex" [disabled]="myFormGroup.invalid">Registrer din konto</ion-button>
        </div>
      </ion-content>

    `,
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent{
  email = new FormControl('',[Validators.required, Validators.maxLength(30),Validators.minLength(5)]);
  username = new FormControl('',[Validators.required, Validators.minLength(5),Validators.maxLength(20)]);
  password = new FormControl('',[Validators.required, Validators.minLength(8),Validators.maxLength(30)]);
  password2 = new FormControl('', [Validators.required, this.matchingPasswords.bind(this)]);

  myFormGroup = new FormControl({
    email: this.email,
    username: this.username,
    password: this.password,
    password2: this.password2,
  });

  // Method that validates that the password matches and ensures no typos in password
  matchingPasswords(control: FormControl): { [key: string]: boolean } | null {
    if (this.password && control.value !== this.password.value) {
      return { 'passwordsNotMatch': true };
    }
    return null;
  }

  constructor(private http : HttpClient) { }

// Method to register the new user
  async registerUser(){
    const registrant = {
      email: this.email,
      username: this.username,
      password: this.password,
    }
    const response = this.http.post<UsersRegister>(environment.baseUrl + '/register', registrant)

      if (response){
        //Todo authentication
      }
  }
}

export interface UsersRegister {
  email: string;
  username: string;
  password: string;
}

