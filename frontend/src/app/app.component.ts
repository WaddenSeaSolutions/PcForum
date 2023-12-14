import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {logOut} from "ionicons/icons";
import {FormControl} from "@angular/forms";

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
        <ion-buttons id="ionButton" (click)="navigateToTwitter()">
          <ion-button>
            <ion-icon id = "icons" name="logo-twitter"></ion-icon>

          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" (click)="navigateToDiscord()">
          <ion-button>
            <ion-icon id = "icons" name="logo-discord"></ion-icon>

          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" (click)="navigateToFacebook()">
          <ion-button>
            <ion-icon id = "icons" name="logo-facebook"></ion-icon>

          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" (click)="navigateToGithub()">
          <ion-button>
            <ion-icon id = "icons" name="logo-github"></ion-icon>

          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%" *ngIf="checkIfLoggedIn" (click)="navigateToProfilePage()">
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
   async logOut(){
    localStorage.clear()
     await this.router.navigate(['home'])
     location.reload();
  }

  async navigateToGithub(){
    window.open('https://github.com/WaddenSeaSolutions');
  }

  async navigateToFacebook(){
    window.open('https://facebook.com');
  }
  async navigateToDiscord(){
    window.open('https://discord.com');
  }
  async navigateToTwitter(){
    window.open('https://twitter.com')
  }

}
