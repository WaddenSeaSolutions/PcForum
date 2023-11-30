import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {logOut} from "ionicons/icons";

@Component({
  selector: 'app-root',
  template: `
    <body>
    <header>
      <h1 id="mainHeadline">PC Forum</h1>
    </header>
    <div id="toolbarHolder">
    <ion-toolbar id="toolbarHeader">
      <ion-item id="toolbarContent">
        <ion-buttons id="ionButton">
          <ion-button (click)="navigateToFrontpage()">
          <ion-icon id = "icons" name="home"></ion-icon>
          <p>Forside</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton">
          <ion-button>
          <ion-icon id = "icons" name="notifications"></ion-icon>
          <p>9+</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%" (click)="navigateToProfilePage()">
          <ion-button>
          <ion-icon id = "icons" name="person"></ion-icon>
          <p>Profil</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%" *ngIf="checkIfLoggedIn" (click)="logOut()">
          <ion-button>
            <ion-icon id = "icons" name="exit-sharp"></ion-icon>
            <p>Log ud</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%" *ngIf="checkIfLoggedOut" (click)="navigateToLoginPage()">
          <ion-button>
          <ion-icon id = "icons" name="enter-sharp"></ion-icon>
          <p>Log in</p>
          </ion-button>
        </ion-buttons>

      </ion-item>
    </ion-toolbar>
    </div>
    </body>
    <router-outlet></router-outlet>
  ` ,
  styleUrls: ['app.component.scss'],
})
export class AppComponent {

  public checkIfLoggedIn: boolean;
  public checkIfLoggedOut: boolean;

  constructor(private router: Router) {
    this.checkIfLoggedIn = localStorage.getItem('token') != null;
    this.checkIfLoggedOut = localStorage.getItem('token') == null;
  }

  async navigateToFrontpage(){
    await this.router.navigate(['home'])
  }

  async navigateToLoginPage(){
    await this.router.navigate(['login-page']);
  }

  async navigateToProfilePage(){
    await this.router.navigate(['profile'])
  }
   logOut(){
    localStorage.clear()
  }

}
