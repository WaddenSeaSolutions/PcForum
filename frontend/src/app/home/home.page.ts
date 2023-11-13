import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  template: `
    <body>
    <header>
      <h1 id="mainHeadline">PC Forum</h1>
    </header>
    <div id="toolbarHeader">
      <ion-item id="toolbarContent">
        <ion-buttons>
          <ion-icon id = "icons" name="home"></ion-icon>
          <p>Forside</p>
        </ion-buttons>

        <ion-icon style = "margin-left: 75%" name="search"></ion-icon>
        <ion-input> </ion-input>

        <ion-buttons>
          <ion-icon id = "icons" name="notifications"></ion-icon>
          <p>9+</p>
        </ion-buttons>
        <ion-buttons style = "margin-left: 0.6%">
          <ion-icon id = "icons" name="person"></ion-icon>
          <p>Profil</p>
        </ion-buttons>
        <ion-buttons style = "margin-left: 0.6%">
          <ion-icon id = "icons" name="exit-sharp"></ion-icon>
          <p>Log ud</p>
        </ion-buttons>


      </ion-item>
    </div>
    </body>
  ` ,
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor() {}

}
