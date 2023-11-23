import { Component } from '@angular/core';
import {Router} from "@angular/router";

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
        <ion-searchbar id = "searchBar"> </ion-searchbar>
        <ion-buttons id="ionButton">
          <ion-button>
          <ion-icon id = "icons" name="notifications"></ion-icon>
          <p>9+</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%">
          <ion-button>
          <ion-icon id = "icons" name="person"></ion-icon>
          <p>Profil</p>
          </ion-button>
        </ion-buttons>
        <ion-buttons id="ionButton" style = "margin-left: 0.6%">
          <ion-button>
          <ion-icon id = "icons" name="exit-sharp"></ion-icon>
          <p>Log ud</p>
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
  constructor(private router: Router) {}

  async navigateToFrontpage(){
    await this.router.navigate(['home'])
  }
}
