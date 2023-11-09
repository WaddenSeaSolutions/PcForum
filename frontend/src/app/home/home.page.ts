import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  template: `
    <body>
    <header>
      <h1>rofl forum</h1>
    </header>
    <div id="toolbarHeader">
      <ion-item>
        <ion-buttons><u>Forside</u></ion-buttons>
        <ion-buttons style = "margin-left: 90%"><u>Profil</u></ion-buttons>
        <ion-buttons style = "margin-left: 1%"><u>Log ud</u></ion-buttons>
      </ion-item>


    </div>
    </body>
  ` ,
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor() {}

}
