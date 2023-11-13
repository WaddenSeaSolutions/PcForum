import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'register',
  template:
    `
    <ion-content>
    <ion-item>
    <ion-input placeholder="Email"> </ion-input>
      <br>
      <ion-input placeholder="Brugernavn"> </ion-input>
      <br>
      <ion-input placeholder="Kodeord"> </ion-input>
      <br>
      <ion-input placeholder="Gentag Kodeord"> </ion-input>
    </ion-item>

    </ion-content>
    `,
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
